using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;

namespace CodeCool.EhotelBuffet.Buffet.Service;

public class BuffetService : IBuffetService
{
    private readonly IMenuProvider _menuProvider;
    private readonly IRefillService _refillService;
    private readonly List<Portion> _currentPortions = new();

    private bool _isInitialized;

    public BuffetService(IMenuProvider menuProvider, IRefillService refillService)
    {
        _menuProvider = menuProvider;
        _refillService = refillService;
        
    }

    public void Refill(IRefillStrategy refillStrategy)
    {
        
    }

    public void Reset()
    {
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

        Portion freshestMeal = matchedMeals[0];
        foreach (var portion in matchedMeals)
        {
            if (portion.TimeStamp > freshestMeal.TimeStamp)
            {
                freshestMeal = portion;
            }
        }

        if (matchedMeals.Count != 0)
        {
            _currentPortions.Remove(freshestMeal);
            return true;
        }
        return false;
    }


    public int CollectWaste(MealDurability mealDurability, DateTime currentDate)
    {
        int cost = 0;
        int durabilityInMins;
        foreach (var portion in _currentPortions)
        {
            if (portion.MenuItem.MealDurability == mealDurability)
            {
                durabilityInMins = portion.MenuItem.MealDurabilityInMinutes;
                if ((currentDate - portion.TimeStamp).TotalMinutes > durabilityInMins)
                {
                    cost += portion.MenuItem.Cost;
                    _currentPortions.Remove(portion);
                }
            }
        }
        return cost;
    }
}
