using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using APIGateway.Model;

namespace APIGateway.Services
{
    public class MessageHub : Hub
    {
        private string _eventCommandGroup = "events";
        private string _notifyMethod = "new_event";

        public async Task Notify(SensorEvent data)
        {
            await Clients.All.SendAsync(_notifyMethod, data); //Group(_eventCommandGroup)
        }

        public async Task<string> JoinGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _eventCommandGroup);
            return "Joined group " + _eventCommandGroup;
        }

        public async Task<string> LeaveGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _eventCommandGroup);
            return "Left group " + _eventCommandGroup;
        }
    }
}
