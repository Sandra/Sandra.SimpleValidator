namespace Sandra.SimpleValidator.Rules
{
    public class Between : IRule
    {
        private dynamic _max;
        private dynamic _min;

        public Between(int min, int max)
        {
            _min = min;
            _max = max;
            Message = string.Format("Field needs to be between {0} and {1}", min, max);
        }

        public Between(decimal min, decimal max)
        {
            _min = min;
            _max = max;
            Message = string.Format("Field needs to be between {0} and {1}", min, max);
        }

        public Between(float min, float max)
        {
            _min = min;
            _max = max;
            Message = string.Format("Field needs to be between {0} and {1}", min, max);
        }

        public Between(double min, double max)
        {
            _min = min;
            _max = max;
            Message = string.Format("Field needs to be between {0} and {1}", min, max);
        }

        public bool IsValid(dynamic value)
        {
            if (value > _min && value < _max)
                return true;

            return false;
        }

        public string Message { get; set; }

        public IRule WithMessage(string message)
        {
            Message = message;

            return this;
        }

        public IRule IsInclusive(bool isInclusive = false)
        {
            if (isInclusive)
            {
                _min -= 1;
                _max += 1;
            }

            return this;
        }
    }
}