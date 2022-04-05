using DontWreckMyHouse.Core.Models;
using System.Collections.Generic;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IGuestRepository
    {
        List<Guest> GetAll();
    }
}
