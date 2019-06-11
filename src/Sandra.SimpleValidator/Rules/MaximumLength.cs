namespace Sandra.SimpleValidator.Rules
{
    public class MaximumLength : IRule
    {
        private readonly int _length;

        public MaximumLength(int length)
        {
            _length = length;
            Message = "Field needs to be Maximum length of " + length;
        }

        public bool IsValid(object value)
        {
            if (value == null)
                return true;

            return ((string) value).Length <= _length;
        }

        public string Message { get; set; }

        public IRule WithMessage(string message)
        {
            Message = message;

            return this;
        }
    }
}