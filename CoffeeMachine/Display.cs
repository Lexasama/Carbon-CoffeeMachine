namespace CoffeeMachine
{
    public class Display
    {
        private string Message;

        public string GetMessage()
        {
            return Message;
        }

        public void SetMessage(string message)
        {
            this.Message = message;
        }
    }
}