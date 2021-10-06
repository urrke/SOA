using APIGateway.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread.Sleep(TimeSpan.FromSeconds(30));
            /*IAppContext appContext = new AppContext();
            IHubContext<MessageHub> hubContext = new HubContext<MessageHub>();
            var client = appContext.MqttClient; //starts listening
            IMessageBrokerService msgService = new MessageBrokerService(appContext);
            IAPIGatewayService analyticsService = new APIGatewayService( msgService, appContext, hubContext);
            analyticsService.StartListening();*/
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
