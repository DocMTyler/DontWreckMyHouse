using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.DAL;
using DontWreckMyHouse.BLL;

namespace DontWreckMyHouse.UI
{
    class NinjectContainer
    {
        public static StandardKernel kernel { get; private set; }

        public static void Configure()
        {
            kernel = new StandardKernel();

            kernel.Bind<ConsoleIO>().To<ConsoleIO>();
            kernel.Bind<View>().To<View>();

            //kernel.Bind<IReservationRepository>().To<ReservationRepository>();

            kernel.Bind<ReservationService>().To<ReservationService>();
            kernel.Bind<HostLocationService>().To<HostLocationService>();
            kernel.Bind<GuestService>().To<GuestService>();
        }
    }
}
