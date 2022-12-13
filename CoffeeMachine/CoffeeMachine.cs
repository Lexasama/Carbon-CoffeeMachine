namespace CoffeeMachine
{
    public class CoffeeMachine
    {
        private readonly IDrinkMaker _drinkMaker;
        private readonly IMoneyService _moneyService;
        private readonly IOrderService _orderService;

        public CoffeeMachine(IOrderService orderService, IDrinkMaker drinkMaker, IMoneyService moneyService)
        {
            _orderService = orderService;
            _drinkMaker = drinkMaker;
            _moneyService = moneyService;
        }

        public void MakeDrink(Command command)
        {
            Drink selectedDrink = Drinks.GetDrink(command.Code);
            if (_moneyService.IsEnough(selectedDrink, command.Cash))
            {
                _drinkMaker.MakeDrink(_orderService.CreateCommand(command.Code, command.Sugar, command.ExtraHot));
            }
            else
            {
                _drinkMaker.ForwardMessage("Not Enougth money");
            }
        }
    }
}