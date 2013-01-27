using System;
using System.Runtime.Serialization;

namespace Sandra.SimpleValidator
{
    [Serializable]
    public class SandraValidationException : Exception
    {
        public SandraValidationException(ValidationResult result)
        {
            Result = result;
        }

        public SandraValidationException(string message, ValidationResult result)
            : base(message)
        {
            Result = result;
        }

        protected SandraValidationException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }

        public ValidationResult Result { get; set; }
    }
}