namespace CoffeeMachine
{
    public interface IDrinkMaker
    {
        void MakeDrink(string instruction);
        void ForwardMessage(string message);
        
    }
}