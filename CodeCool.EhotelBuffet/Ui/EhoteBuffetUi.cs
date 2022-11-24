using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;
using CodeCool.EhotelBuffet.Simulator.Service;

namespace CodeCool.EhotelBuffet.Ui;

public class EhoteBuffetUi
{
    private readonly IReservationManager _reservationManager;
    private readonly IDiningSimulator _diningSimulator;
    private readonly IGuestProvider _guestProvider;
    private readonly IReservationProvider _reservationProvider;

    public EhoteBuffetUi(
        IReservationProvider reservationProvider,
        IReservationManager reservationManager,
        IDiningSimulator diningSimulator,
        IGuestProvider guestProvider)
    {
        _reservationProvider = reservationProvider;
        _reservationManager = reservationManager;
        _diningSimulator = diningSimulator;
        _guestProvider = guestProvider;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the Best Hotel And Buffet!");
        Console.ReadLine();
        int guestCount = 20;
        DateTime seasonStart = DateTime.Today;
        DateTime seasonEnd = DateTime.Today.AddDays(3);
        
        var guests = GetGuests(guestCount);

        CreateReservations(guests, seasonStart, seasonEnd);

        PrintGuestsWithReservations();


        var currentDate = seasonStart;

        while (currentDate <= seasonEnd)
        {
            var simulatorConfig = new DiningSimulatorConfig(
                currentDate.AddHours(6),
                currentDate.AddHours(10),
                30,
                3);

            var results = _diningSimulator.Run(simulatorConfig);
            PrintSimulationResults(results);
            currentDate = currentDate.AddDays(1);
        }

        Console.ReadLine();
    }
    
    private IEnumerable<Guest> GetGuests(int guestCount)
    {
        return _guestProvider.Provide(guestCount);

    }

    private void CreateReservations(IEnumerable<Guest> guests, DateTime seasonStart, DateTime seasonEnd)
    {
        foreach (var guest in guests)
        {
            _reservationManager.AddReservation(_reservationProvider.Provide(guest, seasonStart, seasonEnd));
        }
    }

    private void PrintGuestsWithReservations()
    {
        foreach (var reservation in _reservationManager.GetAll())
        {

            Console.WriteLine($"{reservation.Guest.Name} - " +
                              $"Start: " +
                              $"{reservation.Start}, " +
                              $"End: " +
                              $"{reservation.End}, " +
                              $"Guest Type: " +
                              $"{reservation.Guest.GuestType}");

        }
        

    }

    private static void PrintSimulationResults(DiningSimulationResults results)
    {
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine($"Date: {results.Date}");
        Console.WriteLine($"Total Guests: {results.TotalGuests}");
        Console.WriteLine($"Food waste cost: {results.FoodWasteCost}");
        Console.WriteLine($"Happy guests: {results.HappyGuests.Count()}");
        Console.WriteLine($"Unhappy guests: {results.UnhappyGuests.Count()}");
    }
}
