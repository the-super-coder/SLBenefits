using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using SLBenefits.Core;
using SLBenefits.Core.Helper;

namespace SLBenefits.App_Start
{
    public static class Bootstrapper
    {
        public static void Start()
        {
            // AutoMapperConfiguration.Configure();
            SetAutofacContainer();
        }
        private static void SetAutofacContainer()
        {
            //Create Autofac builder
            var builder = new ContainerBuilder();
            //Now register all depedencies to your custom IoC container

            //register mvc controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
               .AsImplementedInterfaces();

            builder.RegisterModelBinderProvider();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //types

            builder.RegisterType<SLBenefitsContext>().As<DbContext>().InstancePerRequest();
            builder.Register(c => new SLBenefitsContext()).InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterType<ConfigurationHelper>().As<IConfigurationHelper>().InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>))
                   .As(typeof(IRepository<>)).InstancePerRequest();

            //Service
            //builder.RegisterAssemblyTypes(typeof(QuestionService).Assembly)
            //       .Where(t => t.Name.EndsWith("Service"))
            //       .AsImplementedInterfaces().InstancePerRequest();

            var containerBuilder = builder.Build();

            //MVC resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder));

            // Create the depenedency resolver for Web Api
            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder);
            var config = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder);
        }

    }
}