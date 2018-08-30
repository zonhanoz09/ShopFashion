using Autofac;
using Autofac.Integration.WebApi;
using ShopFashion.Model;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using ShopFashion.Repository.Shared;

namespace ShopFashion.WebApi
{
    /// <summary>
    /// Autofac configuration
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Config function
        /// </summary>
        /// <param name="config"></param>
        public static IContainer Configure(HttpConfiguration config)
        {
            // Base set-up
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Register dependencies
            SetUpRegistration(builder);

            // Build registration.
            var container = builder.Build();

            // Set the dependency resolver to be Autofac.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }

        private static void SetUpRegistration(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("ShopFashion.Repository"))
                .Where(t => t.Name.Contains("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("ShopFashion.Service"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(ShopFashionContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
           // builder.RegisterType(typeof(DefaultCacheProvider)).As(typeof(ICacheProvider)).InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            //builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            //builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            //builder.Register(c => new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(c.Resolve<GRnTContext>()))
            //    .AsImplementedInterfaces().InstancePerRequest();
            //builder.Register(c => new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(c.Resolve<GRnTContext>())).AsImplementedInterfaces().InstancePerRequest();

            //builder.RegisterType<SimpleAuthorizationServerProvider>()
            //      .As<IOAuthAuthorizationServerProvider>()
            //      .PropertiesAutowired()
            //      .SingleInstance();

            //builder.RegisterType<SimpleRefreshTokenProvider>()
            //      .As<IAuthenticationTokenProvider>()
            //      .PropertiesAutowired()
            //      .SingleInstance();
        }
    }
}