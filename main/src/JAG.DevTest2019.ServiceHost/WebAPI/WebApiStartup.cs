using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JAG.DevTest2019.ServiceHost.WebAPI
{
    public class WebApiStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

            //Make sure that the API returns useful errors
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            // Adding to the pipeline with our own middleware
            app.Use(async (context, next) =>
            {
                // Add Header
                context.Response.Headers["Product"] = "JAG.Iris Web Api Service";

                // Call next middleware
                await next.Invoke();
            });

            // Custom Middleare
            //No custom middleware app.Use(typeof(CustomMiddleware));

            // Web Api
            app.UseWebApi(config);

        }
    }
}
