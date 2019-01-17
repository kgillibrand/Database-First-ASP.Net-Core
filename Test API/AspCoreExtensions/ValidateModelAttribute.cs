using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CWSTeam.AspCoreExtensions
{
    //Borrowed from https://blog.cloudhub360.com/returning-400-bad-request-from-invalid-model-states-in-asp-net-94275fdfd2a0

    /// <summary>
    /// Implements automatically sending an HTTP 400 (bad request) response when the model state is invalid
    /// </summary>
    internal class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage);
                context.Result = new BadRequestObjectResult(new ErrorResponse(errors));
            }
        }
    }

}
