using System.Collections;
using System.Security.Cryptography;
using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Guests.Model;

public record Guest(string Name, GuestType GuestType) 
{
    public MealType[] MealPreferences { get; set; } = Array.Empty<MealType>();
    

    public void SetMealPreference()
    {
        if (GuestType == GuestType.Business)
        {
            MealPreferences = new[] { MealType.ScrambledEggs, MealType.FriedBacon, MealType.Croissant};
        }
        else if (GuestType.Kid == GuestType)
        {
            MealPreferences = new[] { MealType.Pancake, MealType.Muffin, MealType.Cereal, MealType.Milk };
        }
        else
        {
            MealPreferences = new[]
                { MealType.SunnySideUp, MealType.FriedSausage, MealType.MashedPotato, MealType.Bun, MealType.Muffin };
        }
    }
    
}
