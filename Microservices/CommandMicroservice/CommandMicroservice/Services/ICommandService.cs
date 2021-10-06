using CommandMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Services
{
    public interface ICommandService
    {
        void StartListening();
    }
}
