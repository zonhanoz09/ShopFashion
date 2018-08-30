using Microsoft.Owin;
using Owin;
using ShopFashion.Model;
using ShopFashion.Model.Migrations;
using ShopFashion.Service.Configuration;
using ShopFashion.WebApi;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


[assembly: OwinStartup(typeof(Startup))]

namespace ShopFashion.WebApi
{
    /// <summary>
    ///     Start up
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Configuration IAppBuilder
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var container = AutofacConfig.Configure(config);



            //WebApiConfig.Register(config);
            //app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopFashionContext, Configuration>());

            //old
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutomapperConfiguration.Configure();
            //ConfigureOAuthTokenGeneration(app);
            //AuthConfig.Configure(app);

        }

        //private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        //{
        //    // Configure the db context and user manager to use a single instance per request    
        //    app.CreatePerOwinContext(Liquid8Context.Create);
        //    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        //    app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
        //}
    }
}