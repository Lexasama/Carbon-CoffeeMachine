namespace CoffeeMachine
{
    public class OrderService : IOrderService
    {
        public OrderService()
        {
        }

        public string CreateCommand(char drink, int sugar)
        {
            return Command.ConvertToString(drink, sugar);
        }

        public void ForwardMessage(string message)
        {
        }
    }


}