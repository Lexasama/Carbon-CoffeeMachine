namespace CoffeeMachine
{
    public interface IMoneyService
    {
        public bool IsEnough(Drink drink, float money);

        public bool IsEnough(float expected, float given);
    }
}