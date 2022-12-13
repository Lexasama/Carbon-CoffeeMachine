namespace CoffeeMachine
{
    public class CoffeeMachine
    {
        private readonly IDrinkMaker _drinkMaker;
        private readonly IMoneyService _moneyService;
        private readonly IOrderService _orderService;
        private readonly IReportService _reportService;
        private readonly IStockService _stockService;

        public CoffeeMachine(IOrderService orderService, IDrinkMaker drinkMaker, IMoneyService moneyService,
            IReportService reportService, IStockService stockService)
        {
            _orderService = orderService;
            _drinkMaker = drinkMaker;
            _moneyService = moneyService;
            _reportService = reportService;
            _stockService = stockService;
        }

        public void MakeDrink(Command command)
        {
            Drink selectedDrink = Drinks.GetDrink(command.Code);
            if (!_moneyService.IsEnough(selectedDrink, command.Cash))
            {
                SendMessage("Not enough money");
                return;
            }

            if (!CanBeServed(selectedDrink))
            {
                Notify(selectedDrink);
                return;
            }

            _reportService.AddDrink(selectedDrink);
            _drinkMaker.MakeDrink(_orderService.CreateCommand(command.Code, command.Sugar, command.ExtraHot));
            DestockBeverage(selectedDrink);
        }

        private void Notify(Drink drink)
        {
            if (drink.Water > 0)
            {
                _stockService.NotifyMissingWater();
                SendMessage("Missing water");
            }

            if (drink.Milk > 0)
            {
                _stockService.NotifyMissingMilk();
                SendMessage("Missing milk");
            }
        }

        public void SendMessage(string message)
        {
            _drinkMaker.ForwardMessage(message);
        }

        private void DestockBeverage(Drink drink)
        {
            if (drink.Water > 0)
            {
                _stockService.DestockWater();
            }

            if (drink.Milk > 0)
            {
                _stockService.DestockMilk();
            }
        }

        public bool CanBeServed(Drink drink)
        {
            return EnoughWater(drink) && EnoughMilk(drink);
        }

        private bool EnoughMilk(Drink drink)
        {
            if (drink.Milk == 0)
            {
                return true;
            }

            return !_stockService.IsEmpty("milk");
        }

        private bool EnoughWater(Drink drink)
        {
            if (drink.Water == 0)
            {
                return true;
            }

            return !_stockService.IsEmpty("water");
        }
    }
}