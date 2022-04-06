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
            string hostsFilePath = Path.Combine(projectDirectory, "DWMH_Data", "test", "hosts.csv");
            HostRepository repo = new(hostsFilePath);
            HostService service = new(repo);
            Console.WriteLine("Welcome to Do NOT Wreck My House!!");
            foreach (var host in repo.GetAll())
            {
                var hostName = String.Format($"{host.LastName}");
                Console.WriteLine(hostName);
            }
            
            Console.WriteLine(projectDirectory);
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.kernel.Get<Controller>();
            controller.Run();
        }
    }
}
