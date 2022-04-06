using System;
using Ninject;
using System.IO;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.DAL;
using DontWreckMyHouse.Core.Interfaces;

namespace DontWreckMyHouse.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            string reservationDirectory = Path.Combine(projectDirectory, "DWMH_Data", "reservations");
            string hostsFilePath = Path.Combine(projectDirectory, "DWMH_Data", "test", "hosts.csv");
            IReservationRepository repo = new ReservationRepository(reservationDirectory);
            IHostRepository hostRepo = new HostRepository(hostsFilePath);
            HostService service = new(hostRepo);
            Console.WriteLine("Welcome to Do NOT Wreck My House!!");
            
            foreach (var reservation in repo.GetReservationsByHost(service.FindHostByEmail("eyearnes0@sfgate.com")))
            {
                var hostName = String.Format($"{reservation.GuestID}");
                Console.WriteLine(hostName);
            }
            
            Console.WriteLine(projectDirectory);
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.kernel.Get<Controller>();
            controller.Run();
        }
    }
}
