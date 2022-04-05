using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class HostLocation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public decimal WeekdayRate { get; set; }
        public decimal WeekendRate { get; set; }
    }
}
