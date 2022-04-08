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
    }
}
