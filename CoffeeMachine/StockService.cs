namespace CoffeeMachine
{
    public class StockService : IStockService
    {
        private Dictionary<string, int> _stock = new()
        {
            { "water", 0 },
            { "milk", 0 }
        };


        private readonly IEmailNotifier _emailNotifier;
        private readonly IBeverageQuantityChecker _beverageQuantityChecker;

        public StockService(IEmailNotifier emailNotifier, IBeverageQuantityChecker beverageQuantityChecker)
        {
            _emailNotifier = emailNotifier;
            _beverageQuantityChecker = beverageQuantityChecker;
        }

        public void Restock(string beverage, int quantity)
        {
            _stock[beverage] += quantity;
        }

        public void Destock(string beverage)
        {
            if (!IsEmpty(beverage))
            {
                _stock[beverage] -= 1;
            }
            else
            {
                _emailNotifier.NotifyMissingDrink(beverage);
            }
        }

        public bool IsEmpty(string drinkName)
        {
            return _beverageQuantityChecker.IsEmpty(drinkName);
        }

        public void DestockMilk()
        {
            Destock("milk");
        }

        public void DestockWater()
        {
            Destock("water");
        }

        public void NotifyMissingBeverage(string drink)
        {
            _emailNotifier.NotifyMissingDrink(drink);
        }

        public void NotifyMissingMilk()
        {
            NotifyMissingBeverage("milk");
        }

        public void NotifyMissingWater()
        {
            NotifyMissingBeverage("water");

        }
    }
}