using System;

namespace KH.Pepper.Services
{
    public class ExceptionHandle : Exception
    {
        public ExceptionHandle(string message) : base(message)
        {
        }
    }
}
