using DontWreckMyHouse.Core.Models;
using System.Collections.Generic;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHostLocationRepository
    {
        List<HostLocation> GetAll();
    }
}
