using DontWreckMyHouse.Core.Models;
using System.Collections.Generic;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservationsByHost(Host host);
        bool Add(Host host, Reservation reservation);
        bool Update(Host host, Reservation reservation);
        bool Delete(Host host, Reservation reservation);
    }
}
