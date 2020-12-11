using System;

namespace Heritage.Services.Interfaces.Validation
{
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }

        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}