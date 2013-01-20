namespace Sandra.SimpleValidator
{
    public interface IModelValidator
    {
        ValidationResult Validate(object modelToValidate);
    }
}