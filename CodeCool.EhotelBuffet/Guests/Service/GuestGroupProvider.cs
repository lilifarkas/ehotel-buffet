using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class GuestGroupProvider: IGuestGroupProvider
{
    
    public IEnumerable<GuestGroup> SplitGuestsIntoGroups(IEnumerable<Guest> guests, int groupCount, int maxGuestPerGroup)
    {
        var random = new Random();
        var randomGroups = new List<GuestGroup>();
        int randomgroupCount = random.Next(0, groupCount);
        int id = 0;
        for (int i = 0; i < randomgroupCount; i++)
        {
            randomGroups.Add(new GuestGroup(id++, GenerateRandomGuests(guests, maxGuestPerGroup)));
        }
        GuestGroup[] arrayOfGuests = randomGroups.ToArray();
        foreach (var (i, j) in arrayOfGuests)
        {
            Console.WriteLine(i);
            foreach (var x in j)
            {
                Console.WriteLine(x);
            }
        }
        return arrayOfGuests;
    }

    private IEnumerable<Guest> GenerateRandomGuests( IEnumerable<Guest> guests, int max)
    {
        var random = new Random();
        var guestsToArray = guests as Guest[] ?? guests.ToArray();
        var randomGuests = new List<Guest>();
        
        for (int i = 0; i < max; i++)
        {
            var randomGuest = guestsToArray[random.Next(guestsToArray.Length)];
            if (!randomGuests.Contains(randomGuest))
            {
                randomGuests.Add(randomGuest);
            }
            
        }
        Guest[] arrayOfGuests = randomGuests.ToArray();
        return arrayOfGuests;
    }

    
}