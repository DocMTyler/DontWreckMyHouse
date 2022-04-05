using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL
{
    public class GuestRepository : IGuestRepository
    {
        private const string HEADER = "guest_id,first_name,last_name,email,phone,state";
        private readonly string filePath;

        public GuestRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Guest> GetAll()
        {
            var guests = new List<Guest>();
            if (!File.Exists(filePath))
            {
                return guests;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch(Exception e)
            {
                throw new Exception("Could not find guests", e);
            }

            for(int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = Deserialize(fields);
                if(guest != null)
                {
                    guests.Add(guest);
                }
            }
            return guests;
        }

        public List<Guest> GetByState(string state)
        {
            return GetAll().Where(g => g.State == state.ToUpper()).ToList();
        }

        private Guest Deserialize(string[] fields)
        {
            if(fields.Length != 6)
            {
                return null;
            }

            Guest deGuest = new Guest();
            deGuest.ID = int.Parse(fields[0]);
            deGuest.FirstName = fields[1];
            deGuest.LastName = fields[2];
            deGuest.Email = fields[3];
            deGuest.Phone = fields[4];
            deGuest.State = fields[5];
            return deGuest;
        }
    }
}
