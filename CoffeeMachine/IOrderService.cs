namespace CoffeeMachine
{
    public interface IOrderService
    {
        public string CreateCommand(char drink, int sugar, bool extraHot);
    }
}