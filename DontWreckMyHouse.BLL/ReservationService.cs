﻿using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private readonly IReservationRepository reserveRepo;
        private readonly IGuestRepository guestRepo;
        private readonly IHostRepository hostRepo;

        public ReservationService(IReservationRepository reserveRepo, IGuestRepository guestRepo, IHostRepository hostRepo)
        {
            this.reserveRepo = reserveRepo;
            this.guestRepo = guestRepo;
            this.hostRepo = hostRepo;
        }

        public bool Add(Host host, Reservation reservation)
        {
            return reserveRepo.Add(host, reservation);
        }
        
        public List<Reservation> FindReservationsByHost(Host host)
        {
            return reserveRepo.GetReservationsByHost(host);
        }

        public bool Edit(Host host, Reservation reservation)
        {
            return reserveRepo.Update(host, reservation);
        }

        public bool Cancel(Host host, Reservation reservation)
        {
            return reserveRepo.Delete(host, reservation);
        }

        public bool ValiDate(DateTime inDate, DateTime outDate, Host host)
        {
            if(inDate > outDate)
            {
                Console.WriteLine("Check In date cannot be after Check out date. Please choose different dates.");
                return false;
            }
            
            var reservations = FindReservationsByHost(host);
            foreach(var reservation in reservations)
            {
                if((inDate  > reservation.InDate && inDate.Date < reservation.OutDate) 
                    || (outDate > reservation.InDate && outDate.Date < reservation.OutDate))
                {

                    Console.WriteLine("Those dates are already booked for another guest. Please choose different dates.");
                        return false;
                }
            }
            return true;
        }

        public int CalculateBusinessDays(DateTime inDate, DateTime outDate)
        {
            int businessDays = 0;
            for(var day = inDate.Date; day.Date <= outDate.Date; day = day.AddDays(1))
            {
                if(day.DayOfWeek != DayOfWeek.Friday && day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
                {
                    businessDays++;
                }
            }
            return businessDays;
        }
    }
}
