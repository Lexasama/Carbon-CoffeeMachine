namespace CoffeeMachine
{
    public class Drink
    {
        public string Name { get; }
        public char Code { get; }
        public float Price { get; }

        public Drink(string name, char code, float price)
        {
            Name = name;
            Code = code;
            Price = price;
        }
    }
}