using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHostLocationRepository
    {
        HostLocation Get(string query);
        bool Add(HostLocation host);
    }
}
