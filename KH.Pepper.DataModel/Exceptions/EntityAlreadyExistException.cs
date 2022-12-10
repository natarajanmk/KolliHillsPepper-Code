using System;

namespace KH.Pepper.Core.Domain.Exceptions
{
    public class EntityAlreadyExistException : DomainException
    {
        public EntityAlreadyExistException(string message) : base(message)
        {
        }
    }
}
