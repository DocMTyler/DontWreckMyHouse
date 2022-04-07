using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL.Tests.TestDoubles
{
    public class HostRepositoryDouble : IHostRepository
    {
        private List<Host> hosts = new();

        public List<Host> GetAll()
        {
            return new List<Host>(hosts);
        }

        public Host GetHostByEmail(string email)
        {
            return new Host();
        }
    }
}
