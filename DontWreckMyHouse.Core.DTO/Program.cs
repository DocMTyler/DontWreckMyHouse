using System;
using Ninject;

namespace DontWreckMyHouse.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Do NOT Wreck My House!!");
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.kernel.Get<Controller>();
            controller.Run();
        }
    }
}
