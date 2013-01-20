using System.Text.RegularExpressions;

namespace Sandra.SimpleValidator.Rules
{
    public class Email : IRule
    {
        //Email regex from http://hexillion.com/samples/#Regex
        private const string EmailExpression =
            @"^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$";

        private readonly Regex _regex = new Regex(EmailExpression, RegexOptions.IgnoreCase);

        public Email()
        {
            Message = "Invalid email address";
        }

        public bool IsValid(dynamic value)
        {
            if (value == null)
            {
                return true;
            }

            return _regex.IsMatch((string) value);
        }

        public string Message { get; set; }

        public IRule WithMessage(string message)
        {
            Message = message;

            return this;
        }
    }
}