using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DontWreckMyHouse.DAL
{
    public class ReservationRepository : IReservationRepository
    {
        private const string HEADER = "id,start_date,end_date,guest_id,total";
        private readonly string directory;

        public ReservationRepository(string directory)
        {
            this.directory = directory;
        }

        public List<Reservation> GetReservationsByHost(HostLocation host)
        {
            var reservations = new List<Reservation>();
            var path = GetFilePath(host.ID);
            if (!File.Exists(path))
            {
                Console.WriteLine("Watch No reservations with Anthony Bourdain, because this host has no reservations to look at.");
                return reservations;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not read reservations", ex);
            }

            for(int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Reservation reservation = Deserialize(fields);
                if(reservation != null)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;
        }

        public bool Add(HostLocation host, Reservation reservation)
        {
            List<Reservation> reservations = GetReservationsByHost(host);
            int beforeCount = reservations.Count;
            reservation.ID = reservations.Max(r => r.ID) + 1;
            reservations.Add(reservation);
            int afterCount = reservations.Count;
            Write(reservations, host.ID);
            return beforeCount != afterCount;
        }

        public bool Update(HostLocation host, Reservation reservation)
        {
            List<Reservation> reservations = GetReservationsByHost(host);
            for(int i = 0; i < reservations.Count; i++)
            {
                if(reservations[i].ID == reservation.ID)
                {
                    reservations[i] = reservation;
                    Write(reservations, host.ID);
                    return true;
                }
            }
            return false;
        }

        public bool Delete(HostLocation host, Reservation reservation)
        {
            List<Reservation> reservations = GetReservationsByHost(host);
            for(int i = 0; i < reservations.Count; i++)
            {
                if(reservations[i].ID == reservation.ID)
                {
                    reservations.Remove(reservation);
                    Write(reservations, host.ID);
                    return true;
                }
            }
            return false;
        }

        private string GetFilePath(string hostID)
        {
            return Path.Combine(directory, $"{hostID}.csv");
        }

        private string Serialize(Reservation reservation)
        {
            return string.Format("{0},{1:dd/MM/yyyy},{2:dd/MM/yyyy},{3},{4:0.00}",
                    reservation.ID,
                    reservation.InDate,
                    reservation.OutDate,
                    reservation.GuestID,
                    reservation.TotalCost);
        }

        private Reservation Deserialize(string[] fields)
        {
            if (fields.Length != 5)
            {
                return null;
            }

            Reservation result = new();
            result.ID = int.Parse(fields[0]);
            result.InDate = DateTime.Parse(fields[1]);
            result.OutDate = DateTime.Parse(fields[2]);
            result.GuestID = int.Parse(fields[3]);
            result.TotalCost = decimal.Parse(fields[4]);
            return result;
        }

        private void Write(List<Reservation> reservations, string hostID)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(GetFilePath(hostID));
                writer.WriteLine(HEADER);

                if (reservations == null)
                {
                    return;
                }

                foreach (var entry in reservations)
                {
                    writer.WriteLine(Serialize(entry));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not write to reservations", ex);
            }
        }
    }
}
