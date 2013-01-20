using System.Collections.Generic;
using System.Linq;

namespace Sandra.SimpleValidator
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Messages = new List<ValidationError>();
        }

        public List<ValidationError> Messages { get; set; }

        public bool IsValid
        {
            get { return !Messages.Any(); }
        }

        public bool IsInvalid
        {
            get { return Messages.Any(); }
        }
    }
}