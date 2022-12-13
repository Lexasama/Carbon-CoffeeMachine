namespace CoffeeMachine
{
    public interface IStockService
    {
        public void Restock(string beverage, int quantity);
        public void Destock(string beverage);
        bool IsEmpty(string drinkName);
        void DestockMilk();
        
        void DestockWater();
        
        void NotifyMissingBeverage(string drink);
        void NotifyMissingMilk();
        void NotifyMissingWater();
    }
}