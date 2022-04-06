using NUnit.Framework;
using DontWreckMyHouse.Core.Models;
using System.IO;
using System;

namespace DontWreckMyHouse.DAL.Tests
{
    public class GuestRepoTests
    {
        static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
        static string guestsFilePath = Path.Combine(projectDirectory, "DWMH_Data", "test", "guests.csv");
        GuestRepository guestRepo;

        [SetUp]
        public void Setup()
        {
            if (!File.Exists(projectDirectory + @"DWMH_Data\Test"))
            {
                Directory.CreateDirectory(projectDirectory + @"DWMH_Data\Test");
            }

            guestRepo = new GuestRepository(guestsFilePath);
        }

        [Test]
        public void GetAllShouldReturn1000Count()
        {
            Assert.AreEqual(1000, guestRepo.GetAll().Count);
        }
    }
}