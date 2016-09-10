using KnowledgeBase.Data;
using KnowledgeBase.Data.DocumentDB;
using KnowledgeBase.Data.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KnowledgeBase {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            RegisterDependencies();
        }

        private void RegisterDependencies() {
            
            IUnityContainer container = new UnityContainer();
            //container.RegisterType<IDatasource, JsonDataSource>(
            //    new InjectionConstructor(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\knowledge.json"))
            //);

            container.RegisterInstance<string>("AzureEndpoint", Environment.GetEnvironmentVariable(Config.ENDPOINT_ENVIROMENTVARIABLE, EnvironmentVariableTarget.User) ?? Environment.GetEnvironmentVariable(Config.ENDPOINT_ENVIROMENTVARIABLE, EnvironmentVariableTarget.Machine) ?? Environment.GetEnvironmentVariable(Config.ENDPOINT_ENVIROMENTVARIABLE));
            container.RegisterInstance<string>("AzurePrimaryKey", Environment.GetEnvironmentVariable(Config.PRIMARYKEY_ENVIROMENTVARIABLE, EnvironmentVariableTarget.User) ?? Environment.GetEnvironmentVariable(Config.PRIMARYKEY_ENVIROMENTVARIABLE, EnvironmentVariableTarget.Machine) ?? Environment.GetEnvironmentVariable(Config.PRIMARYKEY_ENVIROMENTVARIABLE));

            container.RegisterType(typeof(IDatasource<>),
                       typeof(AzureDocumentDBDatasource<Article>), "KnowledgeDataSource",
                       new InjectionConstructor(new ResolvedParameter<string>("AzureEndpoint"), 
                       new ResolvedParameter<string>("AzurePrimaryKey"),
                       "knowledge",
                       "articles"                       
            ));

            container.RegisterType(typeof(IDatasource<>),
                       typeof(AzureDocumentDBDatasource<CustomUser>), "UserDataSource",
                       new InjectionConstructor(new ResolvedParameter<string>("AzureEndpoint"),
                       new ResolvedParameter<string>("AzurePrimaryKey"),
                       "knowledge",
                       "users"
            ));           

            container.RegisterType<KnowledgeRepository>(                
                new InjectionConstructor(container.Resolve<IDatasource<Article>>("KnowledgeDataSource"))
            );

            container.RegisterType<UserService>(
                new InjectionConstructor(container.Resolve<IDatasource<CustomUser>>("UserDataSource"))
            );

            DependencyResolver.SetResolver(new UnityResolver(container));            
        }
    } //class
}