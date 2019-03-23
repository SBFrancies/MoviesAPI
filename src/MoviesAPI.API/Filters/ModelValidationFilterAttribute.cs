using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MoviesApi.Api.Filters
{
    public class ModelValidationFilterAttribute : Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new ModelValidationFilter();
        }

        public bool IsReusable => false;

        private class ModelValidationFilter : IAsyncActionFilter
        {
            public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                ModelStateDictionary modelState = context.ModelState;

                if (modelState.IsValid)
                {
                    return next();
                }

                context.Result =
                    new ObjectResult(new Exception("Invalid model")) { StatusCode = (int)HttpStatusCode.BadRequest };

                return Task.CompletedTask;
            }
        }
    }

}
