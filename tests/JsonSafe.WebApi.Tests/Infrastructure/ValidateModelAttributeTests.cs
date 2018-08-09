namespace JsonSafe.WebApi.Tests.Infrastructure
{
    using System.Collections.Generic;
    using AutoFixture.NUnit3;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using NSubstitute;
    using NUnit.Framework;
    using WebApi.Infrastructure;

    [TestFixture]
    public class ValidateModelAttributeTests
    {
        [Test, AutoData]
        public void ValidateModel_With_Invalid_Model_Should_Return_BadRequestObjectResult(string key, string errorMessage)
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError(key, errorMessage);
            var actionContext = new ActionContext(
                Substitute.For<HttpContext>(),
                Substitute.For<RouteData>(),
                Substitute.For<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Substitute.For<Controller>()
            );

            new ValidateModelAttribute().OnActionExecuting(actionExecutingContext);

            Assert.NotNull(actionExecutingContext.Result as BadRequestObjectResult);
        }

        [Test]
        public void ValidateModel_With_Correct_Model_Should_Return_Null()
        {
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(
                Substitute.For<HttpContext>(),
                Substitute.For<RouteData>(),
                Substitute.For<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Substitute.For<Controller>()
            );

            new ValidateModelAttribute().OnActionExecuting(actionExecutingContext);

            Assert.Null(actionExecutingContext.Result);
        }
    }
}
