using System.Collections;
using System.Security.Cryptography;
using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Guests.Model;

public record Guest(string Name, GuestType GuestType)
{
    public MealType[] MealPreferences { get; } = GuestType switch
    {
        GuestType.Business => new []{ MealType.ScrambledEggs, MealType.FriedBacon, MealType.Croissant},
        GuestType.Kid => new[] { MealType.Pancake, MealType.Muffin, MealType.Cereal, MealType.Milk },
        GuestType.Tourist => new[]
            { MealType.SunnySideUp, MealType.FriedSausage, MealType.MashedPotato, MealType.Bun, MealType.Muffin }
    };

}
