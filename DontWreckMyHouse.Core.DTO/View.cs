using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public class View
    {
        private readonly ConsoleIO io;

        public View(ConsoleIO io)
        {
            this.io = io;
        }

        public string GetMenuChoice()
        {
            io.PrintLine("Main Menu");
            io.PrintLine("=========");
            io.PrintLine("0. Exit");
            io.PrintLine("1. View Reservations for Host");
            io.PrintLine("2. Make a Reservation");
            io.PrintLine("3. Edit a Reservation");
            io.PrintLine("4. Cancel a Reservation");
            io.Print("Select [0-4]:");
            return Console.ReadLine();
        }

        public string GetState()
        {
            io.Print("Enter the state(Two letter abbr.): ");
            return Console.ReadLine().ToUpper();
        }
        
        public string GetHostEmail(List<Host> hosts)
        {
            io.PrintLine("");
            io.Print("Enter the host email: ");
            string email = Console.ReadLine();
            foreach (var host in hosts)
            {
                if (host.Email == email)
                {
                    return email;
                }
            }
            io.PrintLine("Host email not found");
            return "";
        }

        public void EnterToContinue()
        {
            io.ReadString("Press [Enter] to continue.");
        }

        public void DisplayHeader(string message)
        {
            io.PrintLine("");
            io.PrintLine(message);
            io.PrintLine(new string('=', message.Length));
        }

        public void DisplayException(Exception ex)
        {
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
        }

        public void DisplayHosts(List<Host> hosts)
        {
            io.PrintLine("Last Name | Email | Phone | Address | City | State | Postal Code");
            foreach(var host in hosts)
            {
                io.PrintLine($"{host.LastName} | {host.Email} | {host.Phone} | {host.Address} | {host.City} | { host.State} | { host.PostalCode}");
            }
        }

        public void DisplayReservations(List<Reservation> reservations)
        {
            DisplayHeader("Reservation ID | In Date | Out Date | Guest ID | Cost");
            foreach (var reservation in reservations)
            {
                io.PrintLine($"{reservation.ID} | {reservation.InDate:MM/dd/yyyy} | {reservation.OutDate:MM/dd/yyyy} | {reservation.GuestID} | ${reservation.TotalCost:0.00}");
            }
            EnterToContinue();
            Console.Clear();
        }

        public DateTime GetReservationInDate()
        {
            return io.ReadDate("Enter the Check In Date: ");
        }

        public DateTime GetReservationOutDate()
        {
            return io.ReadDate("Enter the Check Out Date: ");
        }

        public void DisplayGuests(List<Guest> guests)
        {
            io.PrintLine("Guest ID | First Name | Last Name | Email | Phone | State");
            foreach (var guest in guests)
            {
                io.PrintLine($"{guest.ID} | {guest.FirstName} | {guest.LastName} | {guest.Email} | {guest.Phone} | { guest.State}");
            }
        }

        public string GetGuestEmail(List<Guest> guests)
        {
            io.PrintLine("");
            io.Print("Enter the guest email: ");
            string email = Console.ReadLine();
            foreach (var guest in guests)
            {
                if (guest.Email == email)
                {
                    return email;
                }
            }
            io.PrintLine("Guest email not found");
            return "";
        }
    }
}
