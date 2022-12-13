namespace CoffeeMachine
{
    public interface IEmailNotifier
    {
        void NotifyMissingDrink(string drink);
    }
}