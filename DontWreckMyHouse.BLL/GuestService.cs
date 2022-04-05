using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DontWreckMyHouse.BLL
{
    public class GuestService
    {
        private readonly IGuestRepository repo;

        public GuestService(IGuestRepository repository)
        {
            this.repo = repository;
        }

        public List<Guest> FindByState(string state)
        {
            return repo.GetAll()
                .Where(g => g.State == state.ToUpper())
                .ToList(); ;
        }

    }
}
