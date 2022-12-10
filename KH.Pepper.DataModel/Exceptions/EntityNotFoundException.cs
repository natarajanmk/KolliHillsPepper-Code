using System;

namespace KH.Pepper.Core.Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
