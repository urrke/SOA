using CommandMicroservice.DTOs;
using CommandMicroservice.Model;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandMicroservice.Services
{
    public class CommandService : ICommandService
    {
        private readonly IMessageBrokerService _msgService;
        private readonly IAppContext _appContext;
        public CommandService(IMessageBrokerService msgService, IAppContext appContext)
        {
            this._msgService = msgService;
            this._appContext = appContext;
        }

        public void StartListening()
        {
            this._msgService.Subscribe(this._appContext.EventsTopic);
        }

        
    }
}
