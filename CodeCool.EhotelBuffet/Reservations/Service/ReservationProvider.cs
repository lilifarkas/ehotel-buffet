﻿using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationProvider : IReservationProvider
{
    public Reservation Provide(Guest guest, DateTime seasonStart, DateTime seasonEnd)
    {
        throw new NotImplementedException();
    }
}