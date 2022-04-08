using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.Models;
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
            }
            catch(Exception e)
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
            view.DisplayHeader("Make a Reservation");
            
            Console.WriteLine("First, let's choose a host...");
            string state = view.GetState();
            List<Host> hosts = hostService.FindByState(state);
            if (hosts.Count == 0)
            {
                Console.WriteLine("No hosts found.");
                return;
            }
            view.DisplayHosts(hosts);
            string email = view.GetHostEmail(hosts);
            if (string.IsNullOrEmpty(email)) return;
            var host = hostService.FindHostByEmail(email);
            
            Console.WriteLine("Now, let's choose the dates...");
            var reservation = new Reservation();
            var inDate = view.GetReservationInDate();
            var outDate = view.GetReservationOutDate();
            while(!reservationService.ValiDate(inDate, outDate, host))
            {
                inDate = view.GetReservationInDate();
                outDate = view.GetReservationOutDate();
            }
            reservation.InDate = inDate;
            reservation.OutDate = outDate;
            var totalDays = (outDate - inDate).Days;
            
            Console.WriteLine("Now, let's choose a guest...");
            string guestState = view.GetState();
            List<Guest> guests = guestService.FindByState(guestState);
            if (guests.Count == 0)
            {
                Console.WriteLine("No guests found.");
                return;
            }
            view.DisplayGuests(guests);
            string guestEmail = view.GetGuestEmail(guests);
            if (string.IsNullOrEmpty(email)) return;
            var guest = guestService.FindGuestByEmail(guestEmail);
            if (string.IsNullOrEmpty(guest.ToString()))
            {
                Console.WriteLine("Guest was not found");
                return;
            }
            reservation.GuestID = guest.ID;

            var businessDays = reservationService.CalculateBusinessDays(inDate, outDate);
            var premiumDays = totalDays - businessDays;
            reservation.TotalCost = (businessDays * host.StandardRate) + (premiumDays * host.WeekendRate);

            Console.WriteLine($"{guest.FirstName} {guest.LastName} will be booked at {host.Address} in {host.City}, {host.State} from {inDate:MM/dd/yyyy} - {outDate:MM/dd/yyyy}");
            Console.WriteLine($"The total cost is ${reservation.TotalCost}. Is this correct[y/n]?");
            
            var confirm = Console.ReadLine().ToLower();
            if(confirm == "y")
            {
                reservationService.Add(host, reservation);
                Console.WriteLine("Reservation added");
            }
            else
            {
                Console.WriteLine("Reservation not added");
            }
        }

        private void ViewReservationsForHost()
        {
            view.DisplayHeader("View Reservations by Host");
            string state = view.GetState();
            List<Host> hosts = hostService.FindByState(state);
            if (hosts.Count == 0)
            {
                Console.WriteLine("No hosts found.");
                return;
            }
            view.DisplayHosts(hosts);
            string email = view.GetHostEmail(hosts);
            if (string.IsNullOrEmpty(email)) return;
            var host = hostService.FindHostByEmail(email);
            
            List<Reservation> reservations = reservationService.FindReservationsByHost(host);
            reservations = reservations.OrderByDescending(r => r.InDate).ToList();
            view.DisplayReservations(reservations);
        }
    }
}
