namespace Sandra.SimpleValidator
{
    public interface IRule
    {
        string Message { get; set; }
        bool IsValid(dynamic value);

        IRule WithMessage(string message);
    }

    public interface ICompareRule
    {
        string Message { get; set; }
        bool IsValid(dynamic value, dynamic valueToCompare);

        ICompareRule WithMessage(string message);
    }
}