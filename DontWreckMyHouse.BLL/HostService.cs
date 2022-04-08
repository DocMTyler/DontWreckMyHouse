using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class HostService
    {
        private readonly IHostRepository repo;

        public HostService(IHostRepository repository)
        {
            this.repo = repository;
        }

        public Host FindHostByEmail(string email)
        {
            List<Host> hostList = repo.GetAll();
            foreach (var host in hostList)
            {
                if (host.Email == email)
                {
                    return host;
                }
            }
            Console.WriteLine("Host not found");
            return new Host();
        }

        public List<Host> FindByState(string state)
        {
            return repo.GetAll()
                .Where(h => h.State == state.ToUpper())
                .ToList(); ;
        }
    }
}
