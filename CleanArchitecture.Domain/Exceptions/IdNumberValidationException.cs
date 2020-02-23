using System;

namespace CleanArchitecture.Domain.Exceptions
{
    public class IdNumberValidationException : Exception
    {
        public IdNumberValidationException() : base("ID number is invalid")
        {
        }
    }
}
