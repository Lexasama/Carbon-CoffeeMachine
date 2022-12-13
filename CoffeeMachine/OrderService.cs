namespace CoffeeMachine
{
    public class OrderService : IOrderService
    {
        public string CreateCommand(char drink, int sugar, bool extraHot)
        {
            return Command.ConvertToString(drink, sugar, extraHot);
        }
    }
}