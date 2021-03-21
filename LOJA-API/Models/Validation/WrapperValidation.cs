using System;

namespace Models.Validation
{
    public class WrapperValidation
    {
        public interface InterfaceValidation
        {
            void validate(dynamic model);
        }

        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }

    }
}
