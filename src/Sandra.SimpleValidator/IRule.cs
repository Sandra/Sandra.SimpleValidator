namespace Sandra.SimpleValidator
{
    public interface IRule
    {
        string Message { get; set; }
        bool IsValid(dynamic value);

        IRule WithMessage(string message);
    }
}