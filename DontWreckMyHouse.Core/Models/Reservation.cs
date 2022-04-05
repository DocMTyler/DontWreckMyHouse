using System;

namespace DontWreckMyHouse.Core.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public DateTime InDate { get; set; }
        public DateTime OutDate { get; set; }
        public int GuestID { get; set; }
        public decimal TotalCost { get; set; }
    }
}
