using DontWreckMyHouse.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public class Controller
    {
        private readonly ReservationService reservationService;
        private readonly GuestService guestService;
        private readonly HostService hostService;
        private readonly View view;

        public Controller(ReservationService reservationService, GuestService guestService, HostService hostService, View view)
        {
            this.reservationService = reservationService;
            this.guestService = guestService;
            this.hostService = hostService;
            this.view = view;
        }

        public void Run()
        {
            view.DisplayHeader("Do NOT Wreck My House!!!");
            try
            {
                RunMenu();
            }catch(Exception e)
            {
                view.DisplayException(e);
            }
            view.DisplayHeader("Aufwiedersehen...");
        }

        private void RunMenu()
        {
            bool running = true;
            string selection;
            do
            {
                selection = view.GetMenuChoice();
                switch (selection)
                {
                    case "0":
                        running = false;
                        break;
                    case "1":
                        ViewReservationsForHost();
                        break;
                    case "2":
                        MakeAReservation();
                        break;
                    case "3":
                        EditAReservation();
                        break;
                    case "4":
                        CancelAReservation();
                        break;
                    default:
                        Console.WriteLine("Input not recognized. Please enter 0-4.");
                        break;
                }
            } while (running);
        }

        private void CancelAReservation()
        {
            throw new NotImplementedException();
        }

        private void EditAReservation()
        {
            throw new NotImplementedException();
        }

        private void MakeAReservation()
        {
            throw new NotImplementedException();
        }

        private void ViewReservationsForHost()
        {
            throw new NotImplementedException();
        }
    }
}
