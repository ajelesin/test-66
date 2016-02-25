namespace MvcApp
{
    using System.Data.Entity;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Infra;
    using Models.DataAccess;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new CreateDatabaseIfNotExists<ExchangeContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ExchangeContext>());
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}