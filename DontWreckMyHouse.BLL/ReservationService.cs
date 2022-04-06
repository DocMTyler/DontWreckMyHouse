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
        private readonly IHostLocationRepository hostRepo;

        public ReservationService(IReservationRepository reserveRepo, IGuestRepository guestRepo, IHostLocationRepository hostRepo)
        {
            this.reserveRepo = reserveRepo;
            this.guestRepo = guestRepo;
            this.hostRepo = hostRepo;
        }

        public bool Add(HostLocation host, Reservation reservation)
        {
            return reserveRepo.Add(host, reservation);
        }
        
        public List<Reservation> FindReservationsByHost(HostLocation host)
        {
            return reserveRepo.GetReservationsByHost(ValidateHost(host));
        }

        public bool Edit(HostLocation host, Reservation reservation)
        {
            return reserveRepo.Update(host, reservation);
        }

        public bool Cancel(HostLocation host, Reservation reservation)
        {
            return reserveRepo.Delete(host, reservation);
        }

        private HostLocation ValidateHost(HostLocation host)
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
            return new HostLocation();
        }
    }
}
