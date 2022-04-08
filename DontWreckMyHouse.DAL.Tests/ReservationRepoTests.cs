using NUnit.Framework;
using DontWreckMyHouse.Core.Models;
using System;
using System.IO;
using System.Collections.Generic;

namespace DontWreckMyHouse.DAL.Tests
{
    public class ReservationRepoTests
    {
        static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
        static string reservationsDirectory = Path.Combine(projectDirectory, "DWMH_Data", "test", "reservations");
        static string hostsFilePath = Path.Combine(projectDirectory, "DWMH_Data", "test", "hosts.csv");

        ReservationRepository reserveRepo;
        HostRepository hostRepo;

        List<Reservation> testReservationsList = new();
        Reservation testReservation;

        [SetUp]
        public void Setup()
        {
            if (!File.Exists(projectDirectory + @"DWMH_Data\Test"))
            {
                Directory.CreateDirectory(projectDirectory + @"DWMH_Data\Test");
            }

            reserveRepo = new ReservationRepository(reservationsDirectory);
            hostRepo = new HostRepository(hostsFilePath);
           
            testReservationsList = reserveRepo.GetReservationsByHost(hostRepo.GetHostByEmail("eyearnes0@sfgate.com"));
            testReservation = testReservationsList[0];
        }

        [Test]
        public void GetReservationsByHostShouldReturnCorrectGuestID()
        {
            var reservationsList = reserveRepo.GetReservationsByHost(hostRepo.GetHostByEmail("eyearnes0@sfgate.com"));
            var testGuestID = reservationsList[0].GuestID;

            Assert.AreEqual(410, testGuestID);
        }

        [Test]
        public void AddShouldReturnTrue()
        {
            Assert.IsTrue(reserveRepo.Add(hostRepo.GetHostByEmail("eyearnes0@sfgate.com"), testReservation));
        }

        [Test]
        public void UpdateShouldReturnTrue()
        {
            Assert.IsTrue(reserveRepo.Update(hostRepo.GetHostByEmail("eyearnes0@sfgate.com"), testReservation));
        }

        [Test]
        public void DeleteShouldReturnTrue()
        {
            Assert.IsTrue(reserveRepo.Delete(hostRepo.GetHostByEmail("eyearnes0@sfgate.com"), testReservation));
        }
    }
}
