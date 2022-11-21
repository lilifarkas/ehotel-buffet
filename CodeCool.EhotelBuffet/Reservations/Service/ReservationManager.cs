using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationManager : IReservationManager
{
    public void AddReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Guest> GetGuestsForDate(DateTime date)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Reservation> GetAll()
    {
        throw new NotImplementedException();
    }
}