using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationManager : IReservationManager
{
    private List<Reservation> _guests;

    public ReservationManager()
    {
        _guests = new List<Reservation>();
    }
    public void AddReservation(Reservation reservation)
    {
        _guests.Add(reservation);
    }

    public IEnumerable<Guest> GetGuestsForDate(DateTime date)
    {
        List<Guest> guestsOnDate = new List<Guest>();

        //Find guests whose at a given date
        foreach (var reservation in _guests)
        {
            if (reservation.Start <= date && reservation.End >= date)
            {
                //Add guests whose stay falls on to the date parameter
                guestsOnDate.Add(reservation.Guest);
            }
        }

        return guestsOnDate;
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _guests;
    }
}