namespace CoffeeMachine
{
    public interface IReportService
    {
        string GetReport();

        void AddDrink(Drink drink);
    }
}