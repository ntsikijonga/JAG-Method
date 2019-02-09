using JAG.DevTest2019.ServiceHost.WebAPI;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JAG.DevTest2019.ServiceHost
{
    class Program
    {
        private static IDisposable _WebApiServiceHost;

        static void Main(string[] args)
        {
            Console.WriteLine($"Starting WebAPI");
            int apiPort = 8099;
            string apiHost = "http://localhost";
            string url = $"{apiHost}:{apiPort}";

            _WebApiServiceHost = WebApp.Start<WebApiStartup>(url);
            Console.WriteLine($"WebAPI hosted on {url}");


            Console.WriteLine($"Press enter to exit");
            Console.ReadLine();
        }
    }
}
