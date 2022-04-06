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
        //const string TEST_PATH = @"DWMH_Data\test\guests.csv";
        GuestRepository guestRepo = new GuestRepository(guestsFilePath);

        [SetUp]
        public void Setup()
        {
            //if(!File.Exists(TEST_DIRECTORY + @"DWMH_Data\Test"))
            //{
            //    Directory.CreateDirectory(TEST_DIRECTORY + @"DWMH_Data\Test");
            //}

            //GuestRepository guestRepo = new GuestRepository(TEST_DIRECTORY + TEST_PATH);
        }

        [Test]
        public void GetAllShouldReturn1000Count()
        {
            Assert.AreEqual(1000, guestRepo.GetAll().Count);
        }
    }
}