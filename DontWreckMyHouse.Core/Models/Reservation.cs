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

        public override bool Equals(object obj)
        {
            return obj is Reservation reservation &&
                   ID == reservation.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }
    }
}
