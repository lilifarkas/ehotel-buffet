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
        // var countingScrambledEggs = currentPortions.Count(e => e.MenuItem.MealType == MealType.ScrambledEggs);
        // var countingSunnySideUp= currentPortions.Count(e => e.MenuItem.MealType == MealType.SunnySideUp);
        // var countingFriedSausage= currentPortions.Count(e => e.MenuItem.MealType == MealType.FriedSausage);
        // var countingFriedBacon= currentPortions.Count(e => e.MenuItem.MealType == MealType.FriedBacon);
        // var countingPancake= currentPortions.Count(e => e.MenuItem.MealType == MealType.Pancake);
        // var countingCroissant= currentPortions.Count(e => e.MenuItem.MealType == MealType.Croissant);
        // var countingMashedPotato= currentPortions.Count(e => e.MenuItem.MealType == MealType.MashedPotato);
        // var countingMuffin= currentPortions.Count(e => e.MenuItem.MealType == MealType.Muffin);
        // var countingBun= currentPortions.Count(e => e.MenuItem.MealType == MealType.Bun);
        // var countingCereal= currentPortions.Count(e => e.MenuItem.MealType == MealType.Cereal);
        // var countingMilk= currentPortions.Count(e => e.MenuItem.MealType == MealType.Milk);
        // var countedItem = new List<int>()
        // {
        //     countingScrambledEggs, countingSunnySideUp,countingFriedSausage,countingFriedBacon,countingPancake
        //     ,countingCroissant,countingMashedPotato,countingMuffin,countingBun,countingCereal,countingMilk
        // };
        // var needRefill = new Dictionary<MenuItem, int>();
        // foreach (var item in countedItem)
        // {
        //     var remainder = item - OptimalPortionCount;
        //     if (remainder < 3)
        //     {
        //         needRefill.Add();
        //     }
        // }
        
        return null;
    }

}
