namespace Sandra.SimpleValidator.Rules
{
    public class MinimumLength : IRule
    {
        private readonly int _length;

        public MinimumLength(int length)
        {
            _length = length;
            Message = "Field needs to be minimum length of " + length;
        }

        public bool IsValid(object value)
        {
            if (value == null)
                return false;

            return ((string) value).Length >= _length;
        }

        public string Message { get; set; }

        public IRule WithMessage(string message)
        {
            Message = message;

            return this;
        }
    }
}