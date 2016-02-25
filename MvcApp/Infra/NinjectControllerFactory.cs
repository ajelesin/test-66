namespace MvcApp.Infra
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Models.DataAccess;
    using Ninject;

    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController) _kernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IRepository>().To<SqlRepository>();
        }
    }
}