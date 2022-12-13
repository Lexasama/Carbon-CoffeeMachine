namespace CoffeeMachine
{
    public class ReportService : IReportService
    {
        private Dictionary<Drink, int> _data = new();

        public string GetReport()
        {
            var message = string.Empty;
            foreach (var kp in _data)
            {
                message += $"{kp.Key.Name}: {kp.Value}{Environment.NewLine}";
            }

            message += $"Total: {CalculateMoney()}";
            return message;
        }

        public void AddDrink(Drink drink)
        {
            var counter = _data.GetValueOrDefault(drink, 0);

            if (_data.TryGetValue(drink, out counter))
            {
                _data[drink] = ++counter;
            }
            else
            {
                _data.Add(drink, ++counter);
            }
        }

        private float CalculateMoney()
        {
            float money = 0;

            foreach (var kp in _data)
            {
                money += kp.Key.Price * kp.Value;
            }

            return money;
        }
    }
}