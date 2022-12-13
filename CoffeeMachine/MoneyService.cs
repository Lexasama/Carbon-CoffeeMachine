namespace CoffeeMachine
{
    public class MoneyService : IMoneyService
    {

        public bool IsEnough(Drink drink, float money)
        {
            return money > drink.Price;
        }
    }
}