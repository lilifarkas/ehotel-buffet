using System.Diagnostics.Metrics;
using CodeCool.EhotelBuffet.Menu.Model;

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
        
        foreach (var x in enumerable)
        {
            if (enumerable.Count(portion => portion.MenuItem.MealType == x.MenuItem.MealType) < 3)
            {
                needRefill.Add(x.MenuItem,
                    (OptimalPortionCount -
                     enumerable.Count(portion => portion.MenuItem.MealType == x.MenuItem.MealType)));
            }
        }

        return needRefill;
    }

}
