namespace CoffeeMachine
{
    public interface IOrderService
    {
        public string CreateCommand(char drink, int sugar);
        public void ForwardMessage(string message);
    }
}