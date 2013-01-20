namespace Sandra.SimpleValidator.Rules
{
    public class Required : IRule
    {
        public Required()
        {
            Message = "Field is required";
        }

        public bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace((string) value);
        }

        public string Message { get; set; }

        public IRule WithMessage(string message)
        {
            Message = message;

            return this;
        }
    }
}