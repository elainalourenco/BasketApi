using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Api;
using ShoppingCart.Api.Controllers;
using ShoppingCart.Api.Repositories;
using ShoppingCart.DataContract;
using Moq;
using System.Net;
using FluentValidation;
using FluentValidation.Results;

namespace ShoppingCart.Api.Tests.Controllers
{
    [TestClass]
    public class BasketsControllerTest
    {
        private BasketsController subject;

        private Mock<IRepository<Basket>> basketRepo;
        private Mock<IValidator<Basket>> basketValidator;
        private Basket basket;

        [TestInitialize]
        public void SetUp()
        {
            basket = new Basket()
            {
                Items = new List<BasketItem>() { 
                new BasketItem() { ProductId = 1, Quantity = 2}, 
                new BasketItem() { ProductId = 2, Quantity = 2}}
            };
            basketRepo = new Mock<IRepository<Basket>>(MockBehavior.Strict);
            basketValidator = new Mock<IValidator<Basket>>(MockBehavior.Strict);
            subject = new BasketsController(basketRepo.Object, basketValidator.Object);
            subject.Request = new HttpRequestMessage();
            subject.Request.SetConfiguration(new HttpConfiguration());
        }

        [TestMethod]
        public void when_creating_a_basket_it_fails_if_basket_already_exists()
        {
            basketValidator.Setup(x => x.Validate(basket)).Returns(new ValidationResult(new List<ValidationFailure>()));
            basketRepo.Setup(x => x.Get(basket.Id)).Returns(basket);

            HttpResponseMessage result = subject.Post(basket);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            basketRepo.Verify(x => x.Get(basket.Id));
        }

        [TestMethod]
        public void when_creating_a_basket_it_fails_if_none_items_added()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns(basket);
            basketValidator.Setup(x => x.Validate(basket)).Returns(new ValidationResult(new List<ValidationFailure>()));

            HttpResponseMessage result = subject.Post(basket);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            basketValidator.Verify(x => x.Validate(basket));
        }

        [TestMethod]
        public void when_creating_a_basket_success()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns((Basket) null);
            basketRepo.Setup(x => x.InsertOrUpdate(basket)).Returns(basket);
            basketValidator.Setup(x => x.Validate(basket)).Returns(new ValidationResult(new List<ValidationFailure>()));

            HttpResponseMessage result = subject.Post(basket);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.Created);
        }

        [TestMethod]
        public void when_updating_a_basket_it_fails_if_basket_does_not_exist()
        {
            basketValidator.Setup(x => x.Validate(basket)).Returns(new ValidationResult(new List<ValidationFailure>()));
            basketRepo.Setup(x => x.Get(basket.Id)).Returns((Basket) null);

            HttpResponseMessage result = subject.Put(basket);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            basketRepo.Verify(x => x.Get(basket.Id));
        }

        [TestMethod]
        public void when_updating_a_basket_it_fails_if_validation_fails()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns(basket);
            basketValidator.Setup(x => x.Validate(basket)).Returns(
                new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("Property","Error")
                })
            );

            HttpResponseMessage result = subject.Put(basket);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            basketValidator.Verify(x => x.Validate(basket));
        }

        [TestMethod]
        public void when_updating_a_basket_success()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns(basket);
            basketRepo.Setup(x => x.InsertOrUpdate(basket)).Returns(basket);
            basketValidator.Setup(x => x.Validate(basket)).Returns(new ValidationResult(new List<ValidationFailure>()));

            HttpResponseMessage result = subject.Put(basket);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void when_deleting_a_basket_it_fails_if_basket_does_not_exist()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns((Basket)null);

            HttpResponseMessage result = subject.Delete(basket.Id);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
            basketRepo.Verify(x => x.Get(basket.Id));
        }

        [TestMethod]
        public void when_deleting_a_basket_success()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns(basket);
            basketRepo.Setup(x => x.Delete(basket.Id));

            HttpResponseMessage result = subject.Delete(basket.Id);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
        }
    }
}
