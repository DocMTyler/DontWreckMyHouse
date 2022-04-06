using NUnit.Framework;
using System.IO;
using System;

namespace DontWreckMyHouse.DAL.Tests
{
    public class HostRepoTests
    {
        static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
        static string hostsFilePath = Path.Combine(projectDirectory, "DWMH_Data", "test", "hosts.csv");
        HostRepository hostRepo;

        [SetUp]
        public void Setup()
        {
            if (!File.Exists(projectDirectory + @"DWMH_Data\Test"))
            {
                Directory.CreateDirectory(projectDirectory + @"DWMH_Data\Test");
            }

            hostRepo = new HostRepository(hostsFilePath);
        }

        [Test]
        public void GetAllShouldReturn1000Count()
        {
            Assert.AreEqual(1000, hostRepo.GetAll().Count);
        }
    }
}
