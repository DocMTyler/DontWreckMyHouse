using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private readonly IReservationRepository reserveRepo;
        private readonly IGuestRepository guestRepo;
        private readonly IHostRepository hostRepo;

        public ReservationService(IReservationRepository reserveRepo, IGuestRepository guestRepo, IHostRepository hostRepo)
        {
            this.reserveRepo = reserveRepo;
            this.guestRepo = guestRepo;
            this.hostRepo = hostRepo;
        }

        public bool Add(Host host, Reservation reservation)
        {
            return reserveRepo.Add(host, reservation);
        }
        
        public List<Reservation> FindReservationsByHost(Host host)
        {
            return reserveRepo.GetReservationsByHost(ValidateHost(host));
        }

        public bool Edit(Host host, Reservation reservation)
        {
            return reserveRepo.Update(host, reservation);
        }

        public bool Cancel(Host host, Reservation reservation)
        {
            return reserveRepo.Delete(host, reservation);
        }

        private Host ValidateHost(Host host)
        {
            var hostList = hostRepo.GetAll();
            foreach(var repoHost in hostList)
            {
                if(host.ID == repoHost.ID)
                {
                    return repoHost;
                }
            }
            Console.WriteLine("Host not found");
            return new Host();
        }
    }
}
