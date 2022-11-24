using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;

namespace CodeCool.EhotelBuffet.Buffet.Service;

public class BuffetService : IBuffetService
{
    private readonly IMenuProvider _menuProvider;
    private readonly IRefillService _refillService;
    private readonly List<Portion> _currentPortions = new();

    private bool _isInitialized = false;

    public BuffetService(IMenuProvider menuProvider, IRefillService refillService)
    {
        _menuProvider = menuProvider;
        _refillService = refillService;
        
    }

    public void Refill(IRefillStrategy refillStrategy)
    {
        if (!_isInitialized)
        {
            IEnumerable<MenuItem> menu = _menuProvider.MenuItems;
            Dictionary<MenuItem, int> initialQuantities = refillStrategy.GetInitialQuantities(menu);
            foreach (var portion in _refillService.AskForRefill(initialQuantities))
            {
                _currentPortions.Add(portion);
            }

            _isInitialized = true;
        }
        else
        {
            Dictionary<MenuItem, int> refillQuantities = refillStrategy.GetRefillQuantities(_currentPortions);
            foreach (var portion in _refillService.AskForRefill(refillQuantities))
            {
                _currentPortions.Add(portion);
            }
        }

        Reset();
    }

    public void Reset()
    {
        _isInitialized = false;
        
    }

    public bool Consume(MealType mealType)
    {
        List<Portion> matchedMeals = new List<Portion>();
        
        foreach(var portion in _currentPortions)
        {
            if (portion.MenuItem.MealType == mealType)
            {
                matchedMeals.Add(portion);
            }
            
        }
        
        if (matchedMeals.Count != 0)
        {
            Portion freshestMeal = matchedMeals[0];
            foreach (var portion in matchedMeals)
            {
                if (portion.TimeStamp > freshestMeal.TimeStamp)
                {
                    freshestMeal = portion;
                }
            }
            
            _currentPortions.Remove(freshestMeal);
            foreach (var asd in _currentPortions)
            {
                Console.WriteLine(asd);
            }
            Console.WriteLine("??????????????");
            return true;
        }
        return false;
    }


    public int CollectWaste(MealDurability mealDurability, DateTime currentDate)
    {
        int cost = 0;
        int durabilityInMins;
        var removedItems = new List<Portion>();
        foreach (var portion in _currentPortions)
        {
            if (portion.MenuItem.MealDurability == mealDurability)
            {
                durabilityInMins = portion.MenuItem.MealDurabilityInMinutes;
                if ((currentDate - portion.TimeStamp).TotalMinutes > durabilityInMins)
                {
                    cost += portion.MenuItem.Cost;
                    removedItems.Add(portion);
                }
            }
        }

        foreach (var portion in removedItems)
        {
            _currentPortions.Remove(portion);
        }
        
        return cost;
    }
}
