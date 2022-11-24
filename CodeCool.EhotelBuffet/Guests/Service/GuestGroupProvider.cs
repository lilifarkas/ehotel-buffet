using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class GuestGroupProvider: IGuestGroupProvider
{
    
    public IEnumerable<GuestGroup> SplitGuestsIntoGroups(IEnumerable<Guest> guests, int groupCount, int maxGuestPerGroup)
    {
        var randomGroups = new List<GuestGroup>();
        int id = 0;
        var guestsToArray = guests as Guest[] ?? guests.ToArray();
         // Generate random groups with random guests
        for (int i = 0; i < groupCount; i++)
        {
            if (guestsToArray.Length == guests.Count())
            {
                var generateRandomGroup = GenerateRandomGuests(guests, maxGuestPerGroup);
                randomGroups.Add(new GuestGroup(id++, generateRandomGroup));
            }
            else
            {
                var generateRandomGroup = GenerateRandomGuests(guestsToArray, maxGuestPerGroup);
                generateRandomGroup =  generateRandomGroup as Guest[] ?? generateRandomGroup.ToArray();
                guestsToArray = guestsToArray.Where(val =>
                {
                    return val != generateRandomGroup;
                }).ToArray();
            }
        }
        GuestGroup[] arrayOfGuests = randomGroups.ToArray();
        return arrayOfGuests;
    }

    private IEnumerable<Guest> GenerateRandomGuests( IEnumerable<Guest> guests, int max)
    {
         // Generate random guests fot the groups
        var random = new Random();
        var guestsToArray = guests as Guest[] ?? guests.ToArray();
        var randomGuests = new List<Guest>();
        for (int i = 0; i < max; i++)
        {
            var randomGuest = guestsToArray[random.Next(guestsToArray.Length)];
            if (!randomGuests.Contains(randomGuest))
            {
                randomGuests.Add(randomGuest);
                guestsToArray = guestsToArray.Where(val => val != randomGuest).ToArray();
            }
        }
        Guest[] arrayOfGuests = randomGuests.ToArray();
        return arrayOfGuests;
    }

    
}