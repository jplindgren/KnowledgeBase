using KnowledgeBase.Data;
using KnowledgeBase.Data.AzureTableStore;
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

            container.RegisterType<IDatasource, AzureDocumentDBDatasource>(
                new InjectionFactory(x => 
                    new AzureDocumentDBDatasource(
                        Environment.GetEnvironmentVariable(AzureDocumentDBDatasource.ENDPOINT_ENVIROMENTVARIABLE, EnvironmentVariableTarget.User) ?? Environment.GetEnvironmentVariable(AzureDocumentDBDatasource.ENDPOINT_ENVIROMENTVARIABLE, EnvironmentVariableTarget.Machine), 
                        Environment.GetEnvironmentVariable(AzureDocumentDBDatasource.PRIMARYKEY_ENVIROMENTVARIABLE, EnvironmentVariableTarget.User) ?? Environment.GetEnvironmentVariable(AzureDocumentDBDatasource.PRIMARYKEY_ENVIROMENTVARIABLE, EnvironmentVariableTarget.Machine),
                        "knowledge", "articles")
                )
            );

            container.RegisterType<KnowledgeRepository>(
                new InjectionConstructor(container.Resolve<IDatasource>())
            );

            DependencyResolver.SetResolver(new UnityResolver(container));            
        }
    } //class
}