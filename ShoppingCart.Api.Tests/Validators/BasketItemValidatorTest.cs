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

namespace ShoppingCart.Api.Tests.Validators
{
    [TestClass]
    public class BasketItemValidatorTest
    {
        [TestInitialize]
        public void SetUp()
        {
        }

        [Ignore]
        public void it_fails_if_quantity_is_not_at_least_one()
        {
            // placeholder for the test case, not implement due to time constraint
        }
    }
}
