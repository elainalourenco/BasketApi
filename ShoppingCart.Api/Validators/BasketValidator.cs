using FluentValidation;
using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Api.Validators
{
    public class BasketValidator : AbstractValidator<Basket>
    {
        public BasketValidator()
        {
            RuleFor(x => x.Items)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required")
                .Must((x, list) => list.Count > 0).WithMessage("Required");
        }
    }
}