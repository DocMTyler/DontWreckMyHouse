using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
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

        public Guest FindGuestByEmail(string email)
        {
            List<Guest> guestList = repo.GetAll();
            foreach (var guest in guestList)
            {
                if (guest.Email == email)
                {
                    return guest;
                }
            }
            Console.WriteLine("Guest not found");
            return null;
        }

        public List<Guest> FindByState(string state)
        {
            return repo.GetAll()
                .Where(g => g.State == state.ToUpper())
                .ToList(); ;
        }

    }
}
