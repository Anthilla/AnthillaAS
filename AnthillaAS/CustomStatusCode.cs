using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace AnthillaAS
{
    public class PageNotFoundHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        public PageNotFoundHandler(IViewFactory factory)
            : base(factory)
        {
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "error-404");
            response.StatusCode = HttpStatusCode.NotFound;
            context.Response = response;
        }
    }

    //public class ErrorHandler : DefaultViewRenderer, IStatusCodeHandler
    //{
    //    public ErrorHandler(IViewFactory factory)
    //        : base(factory)
    //    {
    //    }

    //    public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
    //    {
    //        return statusCode == HttpStatusCode.InternalServerError;
    //    }

    //    public void Handle(HttpStatusCode statusCode, NancyContext context)
    //    {
    //        var response = RenderView(context, "error-500");
    //        response.StatusCode = HttpStatusCode.NotFound;
    //        context.Response = response;
    //    }
    //}
}