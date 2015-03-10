using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace AnthillaAS {

    public class Bootstrapper : DefaultNancyBootstrapper {

        //protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
        //}

        //protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context) {
        //    base.ConfigureRequestContainer(container, context);

        //    container.Register<IUserMapper, AnthillaCore.UserDatabase>();
        //}

        //protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context) {
        //    var formsAuthConfiguration =
        //        new FormsAuthenticationConfiguration() {
        //            RedirectUrl = "~/login",
        //            UserMapper = requestContainer.Resolve<IUserMapper>(),
        //        };

        //    FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        //}
    }
}