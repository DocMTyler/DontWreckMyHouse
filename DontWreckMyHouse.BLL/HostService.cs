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
            return repo.GetHostByEmail(email);
        }

        public List<Host> FindByState(string state)
        {
            return repo.GetAll()
                .Where(h => h.State == state.ToUpper())
                .ToList(); ;
        }
    }
}
