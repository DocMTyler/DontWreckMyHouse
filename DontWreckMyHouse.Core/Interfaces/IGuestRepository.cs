using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IGuestRepository
    {
        Guest Get(string query);
        bool Add(Guest guest);
    }
}
