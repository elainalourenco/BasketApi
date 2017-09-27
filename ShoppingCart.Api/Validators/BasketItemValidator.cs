using FluentValidation;
using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Api.Validators
{
    public class BasketItemValidator : AbstractValidator<BasketItem>
    {
        public BasketItemValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}