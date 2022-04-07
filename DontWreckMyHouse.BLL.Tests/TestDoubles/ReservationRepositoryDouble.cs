using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DontWreckMyHouse.BLL.Tests.TestDoubles
{
    public class ReservationRepositoryDouble : IReservationRepository
    {
        private readonly List<Reservation> reservations = new();
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;

        Host host = new Host
        {
            ID = "3edda6bc-ab95-49a8-8962-d50b53f84b15",
            LastName = "Yearnes",
            Email = "eyearnes0@sfgate.com",
            Phone = "(806)1783815",
            Address = "3 Nova Trail",
            City = "Amarillo",
            State = "TX",
            PostalCode = 79182,
            StandardRate = 340m,
            WeekendRate = 425m
        };

        public ReservationRepositoryDouble()
        {
            Reservation reservation = new Reservation();
            reservation.ID = 1;
            reservation.InDate = DateTime.Parse("7/31/2021");
            reservation.OutDate = DateTime.Parse("8/7/2021");
            reservation.GuestID = 640;
            reservation.TotalCost = 2550m;
        }

        public bool Add(Host host, Reservation reservation)
        {
            List<Reservation> reservations = GetReservationsByHost(host);
            int beforeCount = reservations.Count;
            reservations.Add(reservation);
            int afterCount = reservations.Count;
            return beforeCount != afterCount;
        }

        public bool Delete(Host host, Reservation reservation)
        {
            List<Reservation> reservations = GetReservationsByHost(host);
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations[i].ID == reservation.ID)
                {
                    reservations.Remove(reservation);
                    return true;
                }
            }
            return false;
        }

        public List<Reservation> GetReservationsByHost(Host host)
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

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Reservation reservation = Deserialize(fields);
                if (reservation != null)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;
        }

        public bool Update(Host host, Reservation reservation)
        {
            List<Reservation> reservations = GetReservationsByHost(host);
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations[i].ID == reservation.ID)
                {
                    reservations[i] = reservation;
                    return true;
                }
            }
            return false;
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

        private string GetFilePath(string hostID)
        {
            return Path.Combine(projectDirectory, "DWMH_Data", "test", "reservations", $"{hostID}.csv");
        }
    }
}
