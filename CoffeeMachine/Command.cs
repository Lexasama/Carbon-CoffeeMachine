using System.Text.RegularExpressions;

namespace CoffeeMachine
{
    public class Command
    {
        private string Name { get; }
        public char Code { get; }
        public int Sugar { get; }
        public bool Stick { get; }
        public float Cash { get; }
        public bool ExtraHot { get; }

        public Command(char code, int sugar, float cash = 0, bool extraHot = false)
        {
            Code = code;
            Sugar = sugar;
            Stick = Sugar > 0;
            Cash = cash;
            ExtraHot = extraHot;
        }

        private readonly Regex sugarReg = new(@"(?<=\:)(.*?)(?=\:)");

        public static string ConvertToString(char code, int sugar, bool extraHot)
        {
            var result = string.Empty;

            if (code == 'O')
            {
                return "O::";
            }

            result = $"{code}";
            if (extraHot)
            {
                result += "h:";
            }
            else
            {
                result += ":";
            }

            if (sugar == 0)
            {
                return result + ":";
            }

            result += $"{sugar}:0";

            return result;
        }
    }
}