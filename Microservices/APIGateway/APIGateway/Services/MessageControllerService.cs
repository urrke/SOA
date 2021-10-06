using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace APIGateway.Services
{
    public class MessageControllerService : BackgroundService
    {
        private readonly IAPIGatewayService _apiService;

        public MessageControllerService(IAPIGatewayService apiService)
        {
            _apiService = apiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _apiService.StartListening();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
