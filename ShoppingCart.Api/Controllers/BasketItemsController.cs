using FluentValidation;
using ShoppingCart.Api.Repositories;
using ShoppingCart.Api.Validators;
using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShoppingCart.Api.Controllers
{
    public class BasketItemsController : ApiController
    {
        private IValidator<BasketItem> basketItemValidator;
        private IRepository<Basket> basketRepo;
        private IRepository<Product> productRepo;

        public BasketItemsController()
        {
            this.basketRepo = HttpContext.Current.Application["BasketsRepo"] as BasketRepository;
            this.productRepo = HttpContext.Current.Application["ProductsRepo"] as ProductRepository;
            this.basketItemValidator = new BasketItemValidator();
        }

        public BasketItemsController(
            IRepository<Basket> basketRepo,
            IRepository<Product> productRepo,
            IValidator<BasketItem> basketItemValidator)
        {
            this.basketRepo = basketRepo;
            this.productRepo = productRepo;
            this.basketItemValidator = basketItemValidator;
        }

        /*
        // GET: api/BasketItem/{basketId}
        public IEnumerable<BasketItem> Get(int basketId)
        {
            Basket basket = this.basketRepo.Get(basketId);
            IEnumerable<BasketItem> result = basket != null ? basket.Items : new List<BasketItem>();
            return result;
        }
        */

        // POST: api/BasketItem
        public HttpResponseMessage Post(BasketItem item)
        {
            Basket basket = this.basketRepo.Get(item.BasketId);
            Product product = this.productRepo.Get(item.ProductId);
            if (basket == null
                || product == null
                || !basketItemValidator.Validate(item).IsValid /*model invalid*/
                || basket.Items.Count(x => x.ProductId == item.ProductId) != 0 /*item already exist*/
                )
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid model");
            }

            basket.Items.Add(item);
            this.basketRepo.InsertOrUpdate(basket);
            return Request.CreateResponse(HttpStatusCode.Created, item);
        }

        // PUT: api/BasketItem/5
        public HttpResponseMessage Put(BasketItem item)
        {
            Basket basket = this.basketRepo.Get(item.BasketId);
            Product product = this.productRepo.Get(item.ProductId);
            if (basket == null
                || product == null
                || !basketItemValidator.Validate(item).IsValid /*model invalid*/
                || basket.Items.Count(x => x.ProductId == item.ProductId) == 0 /*item does not exist*/
                )
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid model");
            }

            basket.Items.Remove(basket.Items.SingleOrDefault(x => x.ProductId == item.ProductId));
            basket.Items.Add(item);
            this.basketRepo.InsertOrUpdate(basket);
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        // DELETE: api/BasketItem/5
        [Route("api/BasketItems/{basketId}/{productId}")]
        public HttpResponseMessage Delete(int basketId, int productId)
        {
            Basket basket = this.basketRepo.Get(basketId);
            if (basket == null
                || basket.Items.Count(x => x.ProductId == productId) == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid model");
            }

            basket.Items.Remove(basket.Items.SingleOrDefault(x => x.ProductId == productId));
            this.basketRepo.InsertOrUpdate(basket);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
