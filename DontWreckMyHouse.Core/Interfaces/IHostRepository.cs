using DontWreckMyHouse.Core.Models;
using System.Collections.Generic;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHostRepository
    {
        Host GetHostByEmail(string email);
        List<Host> GetAll();
    }
}
