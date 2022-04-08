using System;
using System.Collections.Generic;
using System.IO;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL
{
    public class HostRepository : IHostRepository
    {
        private const string HEADER = "id,last_name,email,phone,address,city,state,postal_code,standard_rate,weekend_rate";
        private readonly string filePath;

        public HostRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Host> GetAll()
        {
            var hosts = new List<Host>();
            if (!File.Exists(filePath))
            {
                return hosts;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                throw new Exception("Could not find hosts", e);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Host host = Deserialize(fields);
                if (host != null)
                {
                    hosts.Add(host);
                }
            }
            return hosts;
        }

        public Host GetHostByEmail(string email)
        {
            List<Host> hostList = GetAll();
            foreach(var host in hostList)
            {
                if(host.Email == email)
                {
                    return host;
                }
            }
            Console.WriteLine("Host not found");
            return new Host();
        }

        private Host Deserialize(string[] fields)
        {
            if (fields.Length != 10)
            {
                return null;
            }

            Host host = new Host();
            host.ID = fields[0].Trim();
            host.LastName = fields[1].Trim();
            host.Email = fields[2].Trim();
            host.Phone = fields[3].Trim();
            host.Address = fields[4].Trim();
            host.City = fields[5].Trim();
            host.State = fields[6].Trim();
            host.PostalCode = int.Parse(fields[7]);
            host.StandardRate = decimal.Parse(fields[8]);
            host.WeekendRate = decimal.Parse(fields[9]);

            return host;
        }
    }
}
