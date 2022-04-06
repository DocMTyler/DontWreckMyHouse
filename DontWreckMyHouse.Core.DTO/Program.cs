using Ninject;

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
