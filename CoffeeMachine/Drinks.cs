namespace CoffeeMachine
{
    public static class Drinks
    {
        public static readonly Drink Coffee = new("coffee", 'C', 0.6f, 1, 0);
        public static readonly Drink Tea = new("tea", 'T', 0.4f, 1, 0);
        public static readonly Drink Chocolate = new("chocolate", 'H', 0.6f, 1, 1);
        public static readonly Drink OrangeJuice = new("orange juice", 'O', 0.6f, 0, 0);


        public static Dictionary<char, Drink> DrinksCode = new()
        {
            { Tea.Code, Tea },
            { Coffee.Code, Coffee },
            { Chocolate.Code, Chocolate },
            { OrangeJuice.Code, OrangeJuice },
        };

        public static readonly List<Drink> Menu = new()
        {
            Coffee,
            Tea,
            Chocolate,
            OrangeJuice
        };

        public static Drink GetDrink(char code)
        {
            DrinksCode.TryGetValue(code, out var drink);
            return drink;
        }
    }
}