using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            view.DisplayHeader("Cancel a Reservation");
            
            //Choose host
            Console.WriteLine("First, let's find the reservation by choosing a host...");
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

            //Display the reservations for that host, and ask for a reservation ID to select the reservation
            List<Reservation> reservations = reservationService.FindReservationsByHost(host);
            reservations = reservations.OrderByDescending(r => r.InDate).ToList();
            view.DisplayFutureReservations(reservations);
            var reservationID = view.GetReservationID();
            var toDelete = reservationService.FindReservationByID(host, reservationID);

            Console.WriteLine("Reservation ID | In Date | Out Date | Guest ID | Cost");
            Console.WriteLine($"{toDelete.ID} | {toDelete.InDate:MM/dd/yyyy} | {toDelete.OutDate:MM/dd/yyyy} | {toDelete.GuestID} | ${toDelete.TotalCost}");

            //Confirm cancel
            Console.WriteLine("Are you sure you want to cancel this reservation?[y/n]");
            var confirm = Console.ReadLine().ToLower();
            if (confirm == "y")
            {
                reservationService.Cancel(host, toDelete);
                Console.WriteLine("Reservation deleted");
            }
            else
            {
                Console.WriteLine("Reservation not deleted");
            }
        }

        private void EditAReservation()
        {
            view.DisplayHeader("Edit a Reservation");

            //Choose host
            Console.WriteLine("First, let's find the reservation by choosing a host...");
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

            //Display the reservations for that host, and ask for a reservation ID to select the reservation
            List<Reservation> reservations = reservationService.FindReservationsByHost(host);
            reservations = reservations.OrderByDescending(r => r.InDate).ToList();
            view.DisplayFutureReservations(reservations);
            var reservationID = view.GetReservationID();
            var toEdit = reservationService.FindReservationByID(host, reservationID);

            Console.WriteLine("Reservation ID | In Date | Out Date | Guest ID | Cost");
            Console.WriteLine($"{toEdit.ID} | {toEdit.InDate:MM/dd/yyyy} | {toEdit.OutDate:MM/dd/yyyy} | {toEdit.GuestID} | ${toEdit.TotalCost}");

            //Select the new dates
            Console.WriteLine("Now, let's choose the new dates...");
            toEdit.InDate = view.GetReservationInDate();
            toEdit.OutDate = view.GetReservationOutDate();

            //Calculate new total and confirm
            var totalDays = (toEdit.OutDate - toEdit.InDate).Days;
            var businessDays = reservationService.CalculateBusinessDays(toEdit.InDate, toEdit.OutDate);
            var premiumDays = totalDays - businessDays;
            toEdit.TotalCost = (businessDays * host.StandardRate) + (premiumDays * host.WeekendRate);

            Console.WriteLine($"This reservation at {host.Address} in {host.City}, {host.State} will have the new dates from {toEdit.InDate:MM/dd/yyyy} - {toEdit.OutDate:MM/dd/yyyy}");
            Console.WriteLine($"The total cost is ${toEdit.TotalCost}. Is this correct[y/n]?");

            var confirm = Console.ReadLine().ToLower();
            if (confirm == "y")
            {
                reservationService.Edit(host, toEdit);
                Console.WriteLine("Reservation edited");
            }
            else
            {
                Console.WriteLine("Reservation not edited");
            }
        }

        private void MakeAReservation()
        {
            view.DisplayHeader("Make a Reservation");
            
            //Choose host
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
            if (host == null) return;
            var reservations = reservationService.FindReservationsByHost(host);
            view.DisplayFutureReservations(reservations);
            
            //Choose dates
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
            
            //Choose guest
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
            if (guest == null) return;
            
            reservation.GuestID = guest.ID;

            //Calculate total and confirm
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
