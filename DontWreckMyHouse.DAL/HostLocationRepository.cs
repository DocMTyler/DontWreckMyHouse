﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL
{
    public class HostLocationRepository : IHostLocationRepository
    {
        private const string HEADER = "id,last_name,email,phone,address,city,state,postal_code,standard_rate,weekend_rate";
        private readonly string filePath;

        public HostLocationRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public List<HostLocation> GetAll()
        {
            var hosts = new List<HostLocation>();
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
                HostLocation host = Deserialize(fields);
                if (host != null)
                {
                    hosts.Add(host);
                }
            }
            return hosts;
        }

        public List<HostLocation> GetByState(string state)
        {
            return GetAll().Where(h => h.State == state.ToUpper()).ToList();
        }

        private HostLocation Deserialize(string[] fields)
        {
            if (fields.Length != 6)
            {
                return null;
            }

            HostLocation host = new HostLocation();
            host.ID = fields[0];
            host.LastName = fields[1];
            host.Email = fields[2];
            host.Phone = fields[3];
            host.Address = fields[4];
            host.City = fields[5];
            host.State = fields[6];
            host.PostalCode = int.Parse(fields[7]);
            host.StandardRate = decimal.Parse(fields[8]);
            host.WeekendRate = decimal.Parse(fields[9]);

            return host;
        }
    }
}
