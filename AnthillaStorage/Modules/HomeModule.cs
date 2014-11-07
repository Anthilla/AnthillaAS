using Nancy;

namespace AnthillaStorage
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = x =>
            {
                return View["home"];
            };
        }
    }
}