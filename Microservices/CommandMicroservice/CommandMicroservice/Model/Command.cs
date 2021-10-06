using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Model
{
    public class Command
    {
        public string BeachName { get; set; }
        public string CommandMessage { get; set; }
        public EventType Event { get; set; }

    }
}
