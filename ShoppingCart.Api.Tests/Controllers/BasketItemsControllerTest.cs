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
    public class BasketItemsControllerTest
    {
        private BasketItemsController subject;

        private Basket basket;
        private BasketItem basketItemNew;
        private Mock<IRepository<Basket>> basketRepo;
        private Mock<IRepository<Product>> productRepo;
        private Mock<IValidator<BasketItem>> basketItemValidator;

        [TestInitialize]
        public void SetUp()
        {
            basket = new Basket() { Id = 3, Items = new List<BasketItem>() { new BasketItem() { BasketId = 3, ProductId = 7, Quantity = 16 } } };
            basketItemNew = new BasketItem() { BasketId = basket.Id, ProductId = 23, Quantity = 7 };
            basketRepo = new Mock<IRepository<Basket>>(MockBehavior.Strict);
            productRepo = new Mock<IRepository<Product>>(MockBehavior.Strict);
            basketItemValidator = new Mock<IValidator<BasketItem>>(MockBehavior.Strict);

            subject = new BasketItemsController(basketRepo.Object, productRepo.Object, basketItemValidator.Object);
            subject.Request = new HttpRequestMessage();
            subject.Request.SetConfiguration(new HttpConfiguration());
        }

        [TestMethod]
        public void when_creating_basket_items_it_fails_if_basket_not_found()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns((Basket)null);
            productRepo.Setup(x => x.Get(basketItemNew.ProductId)).Returns((Product)null);

            HttpResponseMessage result = subject.Post(basketItemNew);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void when_creating_basket_items_it_fails_if_product_not_found()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns(basket);
            productRepo.Setup(x => x.Get(basketItemNew.ProductId)).Returns((Product)null);

            HttpResponseMessage result = subject.Post(basketItemNew);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void when_creating_basket_items_it_fails_if_product_already_exists()
        {
            basketItemNew.ProductId = basket.Items.First().ProductId;
            basketRepo.Setup(x => x.Get(basket.Id)).Returns(basket);
            productRepo.Setup(x => x.Get(basketItemNew.ProductId)).Returns((Product)null);

            HttpResponseMessage result = subject.Post(basketItemNew);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void when_creating_basket_items_success()
        {
            basketRepo.Setup(x => x.Get(basket.Id)).Returns(basket);
            productRepo.Setup(x => x.Get(basketItemNew.ProductId)).Returns(new Product());
            basketItemValidator.Setup(x => x.Validate(basketItemNew)).Returns(new ValidationResult(new List<ValidationFailure>()));
            basketRepo.Setup(x => x.InsertOrUpdate(basket)).Returns(basket);

            HttpResponseMessage result = subject.Post(basketItemNew);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.Created);
        }

        [Ignore]
        public void when_updating_a_basket_item_it_fails_if_basket_does_not_exist()
        {
            // placeholder for the test case, not implement due to time constraint
        }

        [Ignore]
        public void when_updating_a_basket_item_it_fails_if_product_does_not_exist()
        {
            // placeholder for the test case, not implement due to time constraint
        }

        [Ignore]
        public void when_updating_a_basket_item_it_fails_if_validation_fails()
        {
            // placeholder for the test case, not implement due to time constraint
        }

        [Ignore]
        public void when_updating_a_basket_item_it_fails_if_product_is_not_in_the_basket()
        {
            // placeholder for the test case, not implement due to time constraint
        }

        [Ignore]
        public void when_updating_a_basket_item_success()
        {
            // placeholder for the test case, not implement due to time constraint
        }

        [Ignore]
        public void when_deleting_a_basket_item_it_fails_if_basket_not_found()
        {
            // placeholder for the test case, not implement due to time constraint
        }

        [Ignore]
        public void when_deleting_a_basket_item_it_fails_if_product_not_found()
        {
            // placeholder for the test case, not implement due to time constraint
        }

        [Ignore]
        public void when_deleting_a_basket_item_success()
        {
            // placeholder for the test case, not implement due to time constraint
        }
    }
}
