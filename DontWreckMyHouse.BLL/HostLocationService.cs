using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class HostLocationService
    {
        private readonly IHostLocationRepository repo;

        public HostLocationService(IHostLocationRepository repository)
        {
            this.repo = repository;
        }

        public List<HostLocation> FindByState(string state)
        {
            return repo.GetAll()
                .Where(h => h.State == state.ToUpper())
                .ToList(); ;
        }
    }
}
