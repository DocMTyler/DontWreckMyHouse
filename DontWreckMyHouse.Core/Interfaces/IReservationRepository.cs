using DontWreckMyHouse.Core.Models;
using System.Collections.Generic;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetAll();
        Reservation Get(string name);
        bool Add(Reservation reservation);
        bool Update(Reservation reservation);
        bool Delete(Reservation reservation);
    }
}
