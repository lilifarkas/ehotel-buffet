using System.Diagnostics.Metrics;
using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;

namespace CodeCool.EhotelBuffet.Refill.Service;

public class BasicRefillStrategy : IRefillStrategy
{
    private const int OptimalPortionCount = 3;
    

    public Dictionary<MenuItem, int> GetInitialQuantities(IEnumerable<MenuItem> menuItems)
    {
        var ret = new Dictionary<MenuItem, int>();
        foreach (var menuItem in menuItems)
        {
            ret.Add(menuItem, OptimalPortionCount);
        }

        return ret;
    }

    public Dictionary<MenuItem, int> GetRefillQuantities(IEnumerable<Portion> currentPortions)
    {
        var enumerable = currentPortions.ToList();
        var needRefill = new Dictionary<MenuItem, int>();
        MenuProvider menuProvider = new MenuProvider();
        IEnumerable<MenuItem> menu = menuProvider.MenuItems;
        
        foreach (var item in menu)
        {
            if (enumerable.Count(portion => portion.MenuItem.MealType == item.MealType) < 3)
            {
                needRefill.Add(item,
                    (OptimalPortionCount -
                     enumerable.Count(portion => portion.MenuItem.MealType == item.MealType)));
            }
        }
        return needRefill;
    }
}
