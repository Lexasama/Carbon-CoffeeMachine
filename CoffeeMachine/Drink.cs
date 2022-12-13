namespace CoffeeMachine
{
    public class Drink
    {
        public string Name { get; }
        public char Code { get; }
        public float Price { get; }
        
        public int Water { get; }
        public int Milk { get; }

        public Drink(string name, char code, float price, int water, int milk)
        {
            Name = name;
            Code = code;
            Price = price;
            Water = water;
            Milk = Milk;

        }
    }
}