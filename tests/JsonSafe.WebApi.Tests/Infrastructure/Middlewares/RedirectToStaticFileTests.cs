namespace JsonSafe.WebApi.Tests.Infrastructure.Middlewares
{
    using System.IO;
    using System.Threading.Tasks;
    using AutoFixture.NUnit3;
    using Microsoft.AspNetCore.Http;
    using NSubstitute;
    using NUnit.Framework;
    using WebApi.Infrastructure.Middlewares;

    [TestFixture]
    public class RedirectToStaticFileTests
    {
        [Test, AutoData]
        public async Task Middleware_Should_Not_Redirect_If_Path_Is_Api(string filename, string apiPrefix, string url)
        {
            filename = $"/{filename}";
            var path = $"/{apiPrefix}/{url}";
            var context = new DefaultHttpContext
            {
                Request = { Path = path }
            };

            var requestDelegate = Substitute.For<RequestDelegate>();
            var middleware = new RedirectToStaticFile(requestDelegate, filename, apiPrefix);
            await middleware.InvokeAsync(context);
            await requestDelegate.Received().Invoke(context);
            Assert.AreEqual(path, context.Request.Path.Value);
        }

        [Test, AutoData]
        public async Task Middleware_Should_Redirect_If_Path_Is_Not_Api(string filename, string apiPrefix, string url)
        {
            filename = $"/{filename}";
            var context = new DefaultHttpContext
            {
                Request = { Path = $"/{url}" }
            };
            var requestDelegate = Substitute.For<RequestDelegate>();
            var middleware = new RedirectToStaticFile(requestDelegate, filename, apiPrefix);
            await middleware.InvokeAsync(context);
            await requestDelegate.Received().Invoke(context);
            Assert.AreEqual(filename, context.Request.Path.Value);
        }

        [Test, AutoData]
        public async Task Middleware_Should_Not_Redirect_If_Path_Is_Static_File(string filename, string apiPrefix, string url)
        {
            filename = $"/{filename}";
            var path = $"/{url}.js";
            var context = new DefaultHttpContext
            {
                Request = { Path = path }
            };
            var requestDelegate = Substitute.For<RequestDelegate>();
            var middleware = new RedirectToStaticFile(requestDelegate, filename, apiPrefix);
            await middleware.InvokeAsync(context);
            await requestDelegate.Received().Invoke(context);
            Assert.AreEqual(path, context.Request.Path.Value);
        }
    }
}
