using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;

namespace CodeCool.EhotelBuffet.Simulator.Service;

public class BreakfastSimulator : IDiningSimulator
{
    private static readonly Random Random = new();

    private readonly IBuffetService _buffetService;
    private readonly IReservationManager _reservationManager;
    private readonly IGuestGroupProvider _guestGroupProvider;
    private readonly ITimeService _timeService;

    private readonly List<Guest> _happyGuests = new();
    private readonly List<Guest> _unhappyGuests = new();

    private int _foodWasteCost;

    public BreakfastSimulator(
        IBuffetService buffetService,
        IReservationManager reservationManager,
        IGuestGroupProvider guestGroupProvider,
        ITimeService timeService)
    {
        _buffetService = buffetService;
        _reservationManager = reservationManager;
        _guestGroupProvider = guestGroupProvider;
        _timeService = timeService;
    }

    public DiningSimulationResults Run(DiningSimulatorConfig config)
    {
        ResetState();
        IRefillStrategy refillStrategy = new BasicRefillStrategy();
        DateTime currentTime = config.Start;
        IEnumerable<Guest> guests = _reservationManager.GetGuestsForDate(currentTime);
        var enumerable = guests as Guest[] ?? guests.ToArray();
        foreach (var guest in enumerable)
        {
            Console.WriteLine("Guests: " + guest);
        }
        int maxGuestsPerGrp = enumerable.Count() / config.MinimumGroupCount;
        
        Console.WriteLine("Guest groups: ");
        if (maxGuestsPerGrp > 0)
        {
            var guestGroups = _guestGroupProvider.SplitGuestsIntoGroups(enumerable, config.MinimumGroupCount, maxGuestsPerGrp);
            
            foreach (var guestGroup in guestGroups)
            {
                Console.WriteLine(guestGroup);
                foreach (var guestGroupGuest in guestGroup.Guests)
                {
                    Console.WriteLine(guestGroupGuest);
                }
            }
            
        }
        _buffetService.Refill(refillStrategy);
        Console.WriteLine("------------------------------------------------------");
        return null;
    }

    private void ResetState()
    {
        _foodWasteCost = 0;
        _happyGuests.Clear();
        _unhappyGuests.Clear();
        _buffetService.Reset();
    }
}
