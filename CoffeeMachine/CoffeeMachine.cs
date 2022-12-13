namespace CoffeeMachine
{
    public class CoffeeMachine
    {
        private readonly IDrinkMaker _drinkMaker;
        private readonly IMoneyService _moneyService;
        private readonly IOrderService _orderService;
        private readonly IReportService _reportService;
        public CoffeeMachine(IOrderService orderService, IDrinkMaker drinkMaker, IMoneyService moneyService, IReportService  reportService)
        {
            _orderService = orderService;
            _drinkMaker = drinkMaker;
            _moneyService = moneyService;
            _reportService = reportService;
        }

        public void MakeDrink(Command command)
        {
            Drink selectedDrink = Drinks.GetDrink(command.Code);
            if (_moneyService.IsEnough(selectedDrink, command.Cash))
            {
                _reportService.AddDrink(selectedDrink);
                _drinkMaker.MakeDrink(_orderService.CreateCommand(command.Code, command.Sugar, command.ExtraHot));
            }
            else
            {
                _drinkMaker.ForwardMessage("Not Enougth money");
            }
        }
    }
}