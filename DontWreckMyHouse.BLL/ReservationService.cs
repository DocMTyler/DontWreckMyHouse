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

        public List<Reservation> FindReservationsByHost(HostLocation host)
        {
            return ValidateHost(host) ? reserveRepo.GetReservationsByHost(host) : new List<Reservation>();
        }

        public bool ValidateHost(HostLocation host)
        {
            var hostList = hostRepo.GetAll();
            foreach(var repoHost in hostList)
            {
                if(host.ID == repoHost.ID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
