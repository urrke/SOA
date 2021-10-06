using AnalyticsMicroservice.Services;
using Data_Microservice;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyticsMicroservice
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Thread.Sleep(TimeSpan.FromSeconds(30));
            IAppContext appContext = new AppContext();
            var client = appContext.MqttClient; //starts listening
            IMongoDbService mongoSerevice = new MongoDbService(appContext);
            IMessageBrokerService msgService = new MessageBrokerService(appContext);
            IAnalyticsService analyticsService = new AnalyticsService(mongoSerevice, msgService, appContext);
            analyticsService.StartListening();
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
