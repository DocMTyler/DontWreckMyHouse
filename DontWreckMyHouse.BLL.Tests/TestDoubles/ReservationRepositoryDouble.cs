using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL.Tests.TestDoubles
{
    public class ReservationRepositoryDouble : IReservationRepository
    {
        private readonly List<Reservation> reservations = new();
        Host host = new Host
        {
            ID = "3edda6bc - ab95 - 49a8 - 8962 - d50b53f84b15",
            LastName = "Yearnes",
            Email = "eyearnes0@sfgate.com",
            Phone = "(806) 1783815",
            Address = "3 Nova Trail",
            City = "Amarillo",
            State = "TX",
            PostalCode = 79182,
            StandardRate = 340m,
            WeekendRate = 425m
        };

        public ReservationRepositoryDouble()
        {
            Reservation reservation = new Reservation();
            reservation.ID = 1;
            reservation.InDate = DateTime.Parse("7/31/2021");
            reservation.OutDate = DateTime.Parse("8/7/2021");
            reservation.GuestID = 640;
            reservation.TotalCost = 2550m;
        }

        public bool Add(Host host, Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Host host, Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservationsByHost(Host host)
        {
            throw new NotImplementedException();
        }

        public bool Update(Host host, Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
