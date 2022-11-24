using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationProvider : IReservationProvider
{
    public Reservation Provide(Guest guest, DateTime seasonStart, DateTime seasonEnd)
    {
        Random rnd = new Random();
        //Generate a random start stay date
        int stayStartDate = rnd.Next(seasonStart.Day, seasonEnd.Day);
        //Generate random stay length, stayStartDate + stayLength can't exceed seasonEnd.
        int stayLength = rnd.Next(1, (seasonEnd.Day - stayStartDate) + 1);

        //Create DateTime objects from calculated dates.
        DateTime startDate = new DateTime(seasonStart.Year, seasonStart.Month, stayStartDate);
        DateTime endDate = new DateTime(seasonStart.Year, seasonStart.Month, stayStartDate + stayLength, 10 ,0 ,0);
        
        return new Reservation(startDate, endDate, guest);
    }
}