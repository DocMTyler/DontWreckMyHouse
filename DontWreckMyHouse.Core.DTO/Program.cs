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
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.kernel.Get<Controller>();
            controller.Run();
        }
    }
}
