using System;

namespace DontWreckMyHouse.Core.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public DateTime InDate { get; set; }
        public DateTime OutDate { get; set; }
        public Guest Guest { get; set; }
        public HostLocation Host { get; set; }
    }
}
