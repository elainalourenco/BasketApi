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
    public class BasketsController : ApiController
    {
        private IRepository<Basket> basketRepo;
        private IValidator<Basket> basketValidator;

        public BasketsController()
        {
            this.basketRepo = HttpContext.Current.Application["BasketsRepo"] as BasketRepository;
            this.basketValidator = new BasketValidator();
        }

        public BasketsController(IRepository<Basket> basketRepo, IValidator<Basket> basketValidator)
        {
            this.basketRepo = basketRepo;
            this.basketValidator = basketValidator;
        }

        // GET: api/basket
        public IEnumerable<Basket> Get()
        {
            throw new NotImplementedException();
        }

        // GET: api/Basket/5
        public HttpResponseMessage Get(int id)
        {
            Basket result = this.basketRepo.Get(id);
            if (result == null)
               return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid model");
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Basket/5
        public HttpResponseMessage Put(Basket value)
        {
            if (!basketValidator.Validate(value).IsValid || this.basketRepo.Get(value.Id) == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid model");

            this.basketRepo.InsertOrUpdate(value);
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        // POST: api/Basket
        public HttpResponseMessage Post(Basket value)
        {
            if (!basketValidator.Validate(value).IsValid || this.basketRepo.Get(value.Id) != null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid model");

            this.basketRepo.InsertOrUpdate(value);
            return Request.CreateResponse(HttpStatusCode.Created, value);
        }

        // DELETE: api/Basket/5
        public HttpResponseMessage Delete(int id)
        {
            if (this.basketRepo.Get(id) == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid model");

            this.basketRepo.Delete(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
