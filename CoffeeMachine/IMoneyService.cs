namespace CoffeeMachine
{
    public interface IMoneyService
    {
        public bool IsEnough(Drink drink, float money);
    }
}