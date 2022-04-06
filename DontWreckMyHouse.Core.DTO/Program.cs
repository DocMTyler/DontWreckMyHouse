using System;
using Ninject;
using System.IO;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.DAL;

namespace DontWreckMyHouse.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            string guestsFilePath = Path.Combine(projectDirectory, "DWMH_Data", "test", "guests.csv");
            GuestRepository repo = new(guestsFilePath);
            GuestService service = new(repo);
            Console.WriteLine("Welcome to Do NOT Wreck My House!!");
            foreach (var guest in repo.GetAll())
            {
                var guestName = String.Format($"{guest.LastName}");
                Console.WriteLine(guestName);
            }
            
            Console.WriteLine(projectDirectory);
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.kernel.Get<Controller>();
            controller.Run();
        }
    }
}
