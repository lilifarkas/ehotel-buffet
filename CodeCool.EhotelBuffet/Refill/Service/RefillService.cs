using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Refill.Service;

public class RefillService : IRefillService
{
    public IEnumerable<Portion> AskForRefill(Dictionary<MenuItem, int> menuItemToPortions)
    {
        var newPortions = new List<Portion>();
        foreach (var (item, quantity) in menuItemToPortions)
        {
            for (int i = 0; i < quantity; i++)
            {
                newPortions.Add(new Portion(item, DateTime.Now.AddDays(-1)));
            }
        }
        
        return newPortions;
    }
}