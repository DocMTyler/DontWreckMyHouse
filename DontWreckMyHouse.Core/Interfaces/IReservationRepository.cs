using DontWreckMyHouse.Core.Models;
using System.Collections.Generic;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservationsByHost(HostLocation host);
        bool Add(HostLocation host, Reservation reservation);
        bool Update(HostLocation host, Reservation reservation);
        bool Delete(HostLocation host, Reservation reservation);
    }
}
