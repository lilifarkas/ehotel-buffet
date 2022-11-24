using System.Collections;
using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class RandomGuestGenerator : IGuestProvider
{
    private static readonly Random Random = new();

    private static readonly string[] Names =
    {
        "Georgia", "Savannah", "Phoenix", "Winona", "Carol", "Brooklyn", "Talullah", "Scarlett", "Ruby", "Lola",
        "Cleo", "Beatrix", "Mika", "Everly", "Kiki", "Rae", "Arya", "Elsa", "Lulu", "Zelda",
        "Felix", "Finn", "Theo", "Hugo", "Archie", "Magnus", "Lucian", "Enzo", "Otto", "Nico", "Rhys",
        "Rupert", "Hugh", "Finley", "Ralph", "Lewis", "Wilbur", "Alfie", "Ernest", "Chester", "Ziggy"
    };

    public IEnumerable<Guest> Provide(int quantity)
    {
        var randomGuests = new List<Guest>();
        for (int i = 0; i < quantity; i++)
        {
            if (!randomGuests.Contains(GenerateRandomGuest()))
            {
                randomGuests.Add(GenerateRandomGuest());
            }
        }

        return randomGuests;
    }

    private static Guest GenerateRandomGuest()
    {
        return new Guest(GetRandomName(), GetRandomType());
    }

    private static string GetRandomName()
    {
        string randomName = Names[Random.Next(Names.Length)];
        return randomName;
    }

    private static GuestType GetRandomType()
    {
        Array values = Enum.GetValues(typeof(GuestType));
        GuestType randomType = (GuestType)(values.GetValue(Random.Next(values.Length)) ?? typeof(GuestType));
        return randomType;
    }
}
