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
        DateTime currentTime = _timeService.SetCurrentTime(config.Start);

        IEnumerable<Guest> guests = _reservationManager.GetGuestsForDate(currentTime);
        
        var enumerable = guests as Guest[] ?? guests.ToArray();
        int maxGuestsPerGrp = enumerable.Count() / config.MinimumGroupCount;
        var guestGroups = _guestGroupProvider.SplitGuestsIntoGroups(enumerable, config.MinimumGroupCount, maxGuestsPerGrp);
        
        
        foreach (var guestGroup in guestGroups)
        {
            _buffetService.Refill(refillStrategy);
            foreach (var guestGroupGuest in guestGroup.Guests)
            {
                
                foreach (var mealPreference in guestGroupGuest.MealPreferences)
                {
                    
                    if (_buffetService.Consume(mealPreference))
                    {
                        if (!_happyGuests.Contains(guestGroupGuest))
                        {
                            _happyGuests.Add(guestGroupGuest);
                        }
                    }
                    else
                    {
                        if (!_unhappyGuests.Contains(guestGroupGuest))
                        {
                            _unhappyGuests.Add(guestGroupGuest);
                        }
                    }
                }
                
            }
                _timeService.IncreaseCurrentTime(config.CycleLengthInMinutes);
                _foodWasteCost += _buffetService.CollectWaste(MealDurability.Short, _timeService.GetCurrentTime());
                _foodWasteCost += _buffetService.CollectWaste(MealDurability.Medium, _timeService.GetCurrentTime());
                _foodWasteCost += _buffetService.CollectWaste(MealDurability.Long, _timeService.GetCurrentTime());
        }
        Console.WriteLine(_foodWasteCost);
        int waste = _foodWasteCost;
        return new DiningSimulationResults(currentTime, enumerable.Count(), waste, _happyGuests, _unhappyGuests);
    }

    private void ResetState()
    {
        _foodWasteCost = 0;
        _happyGuests.Clear();
        _unhappyGuests.Clear();
        _buffetService.Reset();
    }
}
