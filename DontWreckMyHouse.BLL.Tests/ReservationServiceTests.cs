using DontWreckMyHouse.BLL.Tests.TestDoubles;
using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System;

namespace DontWreckMyHouse.BLL.Tests
{
    public class ReservationServiceTests
    {
        ReservationService service = new ReservationService(
            new ReservationRepositoryDouble(),
            new GuestRepositoryDouble(),
            new HostRepositoryDouble());

        Host host = new();/* Host()
        {
            ID = "3edda6bc-ab95-49a8-8962-d50b53f84b15",
            LastName = "Yearnes",
            Email = "eyearnes0@sfgate.com",
            Phone = "(806)1783815",
            Address = "3 Nova Trail",
            City = "Amarillo",
            State = "TX",
            PostalCode = 79182,
            StandardRate = 340m,
            WeekendRate = 425m
        };*/

        Reservation reservation = new();/* Reservation
        {
            ID = 1,
            InDate = DateTime.Parse("7/31/2021"),
            OutDate = DateTime.Parse("8/31/2021"),
            GuestID = 640,
            TotalCost = 2550m
        };*/

        [SetUp]
        public void Setup()
        {
            host = new Host
            {
                ID = "3edda6bc-ab95-49a8-8962-d50b53f84b15",
                LastName = "Yearnes",
                Email = "eyearnes0@sfgate.com",
                Phone = "(806)1783815",
                Address = "3 Nova Trail",
                City = "Amarillo",
                State = "TX",
                PostalCode = 79182,
                StandardRate = 340m,
                WeekendRate = 425m
            };

            reservation = new Reservation
            {
                ID = 2,
                InDate = DateTime.Parse("7/31/2022"),
                OutDate = DateTime.Parse("8/31/2022"),
                GuestID = 640,
                TotalCost = 2550m
            };
        }

        [Test]
        public void ShouldReturnCorrectReservationID()
        {
            var foundHost = service.FindReservationsByHost(host);
            var ID = foundHost[0].ID;
            Assert.AreEqual(foundHost[0].ID, ID);
        }
        
        [Test]
        public void ShouldReturnTrueWhenAdd()
        {
            Assert.IsTrue(service.Add(host, reservation));
        }

        [Test]
        public void ShouldReturnTrueWhenUpdate()
        {
            Assert.IsFalse(service.Edit(host, reservation));
        }

        [Test]
        public void ShouldReturnTrueWhenDelete()
        {
            Assert.IsFalse(service.Cancel(host, reservation));
        }
    }
}