using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleDotnetTemplate.Core.Common.Exceptions;

namespace SimpleDotnetTemplate.Api.Filters
{
    public class EntityNotFoundExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is EntityNotFoundException)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}