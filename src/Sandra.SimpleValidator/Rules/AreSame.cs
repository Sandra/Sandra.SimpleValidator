namespace Sandra.SimpleValidator.Rules
{
    public class AreSame : ICompareRule
    {
        public string Message { get; set; }

        public bool IsValid(dynamic value, dynamic valueToCompareTo)
        {
            return value == valueToCompareTo;
        }

        public ICompareRule WithMessage(string message)
        {
            Message = message;

            return this;
        }
    }
}