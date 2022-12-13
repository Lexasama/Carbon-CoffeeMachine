namespace CoffeeMachine
{
    public static class Drinks
    {
        private static readonly Drink coffee = new("coffee", 'C', 0.6f);
        private static readonly Drink tea = new("tea", 'T', 0.4f);
        private static readonly Drink chocolate = new("chocolate", 'H', 0.6f);
        private static readonly Drink orangeJuice = new("orange juice", 'O', 0.6f);


        public static Dictionary<char, Drink> DrinksCode = new()
        {
            { tea.Code, tea },
            { coffee.Code, coffee },
            { chocolate.Code, chocolate },
            { orangeJuice.Code, orangeJuice },
        };

        public static readonly List<Drink> Menu = new()
        {
            coffee,
            tea,
            chocolate,
            orangeJuice
        };

        public static Drink GetDrink(char code)
        {
            DrinksCode.TryGetValue(code, out var drink);
            return drink;
        }
    }
}