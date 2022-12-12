
namespace CoffeeMachine
{
    public class DrinkMaker : IDrinkMaker
    {
        private readonly IOrderService _orderService;

        public DrinkMaker(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        
        
        public void MakeDrink(string instruction)
        {
           
        }

   
        
    }
}