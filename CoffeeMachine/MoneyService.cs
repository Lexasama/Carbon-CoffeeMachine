namespace CoffeeMachine
{
    public class MoneyService : IMoneyService
    {

        public bool IsEnough(Drink drink, float money)
        {
            return IsEnough(drink.Price, money);
        }

        public bool IsEnough(float expected, float given)
        {
            return given >= expected;
        }
    }
}