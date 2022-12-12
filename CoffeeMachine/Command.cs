using System.Text.RegularExpressions;

namespace CoffeeMachine
{
    public class Command
    {
        private readonly Dictionary<char, string> Drinks = new()
        {
            { 'T', "tea" },
            { 'H', "chocolate" },
            { 'C', "coffee" },
        };

        private string DrinkName { get; set; }
        public char DrinkCode { get; set; }
        public int Sugar { get; set; }
        public bool Stick { get; set; }

        public Command(char drinkCode, int sugar)
        {
            DrinkCode = drinkCode;
            Sugar = sugar;
            Stick = Sugar > 0;
        }

        public string GetDrinkName()
        {
            Drinks.TryGetValue(DrinkCode, out var key);
            return key;
        }

        public string GetDrinkName(char code)
        {
            Drinks.TryGetValue(code, out var key);
            return key;
        }


        private readonly Regex sugarReg = new(@"(?<=\:)(.*?)(?=\:)");

        public static string ConvertToString(Command command)
        {
            var result = string.Empty;

            result = $"{command.DrinkCode}:";
            if (command.Sugar == 0)
            {
                return result + ":";
            }

            result += $"{command.Sugar}:0";

            return result;
        }

        public static string ConvertToString(char drinkCode, int sugar)
        {
            return ConvertToString(new Command(drinkCode, sugar));
        }

        public Command ConvertToCommand(string commands)
        {
            var drinkCode = commands[0];
            var sugarMatch = sugarReg.Match(commands);

            var sugar = 0;
            if (sugarMatch.Success)
            {
                sugar = int.Parse(sugarMatch.Value);
            }
            //
            // var stick = commands[^1] != ':';

            return new Command(drinkCode, sugar);
        }
    }
}