using KH.Pepper.Core.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.Pepper.Web.Config
{
    public class ExceptionHandlingOptions
    {
        public Dictionary<Type, Func<HttpContext, Exception, ProblemDetailsMapping>> Mappers { get; } = new Dictionary<Type, Func<HttpContext, Exception, ProblemDetailsMapping>>();
            

        public void Map<TException>(Func<HttpContext, Exception,ProblemDetailsMapping> mapping) where TException : DomainException
        {
            Mappers.Add(typeof(TException), (ctx, ex) => mapping(ctx, (TException)ex));
        }
    }
}
