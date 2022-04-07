using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL.Tests.TestDoubles
{
    public class GuestRepositoryDouble : IGuestRepository
    {
        List<Guest> guests = new();
        
        public List<Guest> GetAll()
        {
            return new List<Guest>(guests);
        }
    }
}
