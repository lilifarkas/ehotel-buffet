using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Refill.Service;

namespace CodeCool.EhotelBuffet.Buffet.Service;

public interface IBuffetService
{
    public void GetPortions();
    void Refill(IRefillStrategy refillStrategy);
    bool Consume(MealType mealType);
    int CollectWaste(MealDurability mealDurability, DateTime currentDate);
    void Reset();
}
