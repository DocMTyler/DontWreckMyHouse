using System;
using Ninject;
using DontWreckMyHouse.BLL;
using System.IO;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.DAL;

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

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            string reservationDirectory = Path.Combine(projectDirectory, "DWMH_Date", "reservations");
            string guestsFilePath = Path.Combine(projectDirectory, "DWMH_Date", "guests.csv");
            string hostsFilePath = Path.Combine(projectDirectory, "DWMH_Date", "hosts.csv");

            kernel.Bind<IReservationRepository>().To<ReservationRepository>().WithConstructorArgument(reservationDirectory);
            kernel.Bind<IGuestRepository>().To<GuestRepository>().WithConstructorArgument(guestsFilePath);
            kernel.Bind<IHostRepository>().To<HostRepository>().WithConstructorArgument(hostsFilePath);

            kernel.Bind<ReservationService>().To<ReservationService>();
            kernel.Bind<HostService>().To<HostService>();
            kernel.Bind<GuestService>().To<GuestService>();
        }
    }
}
