﻿using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CashFlowException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnkownError(context);

            }
        }

        private void HandleProjectException(ExceptionContext context) 
        {
            if (context.Exception is ErrorOnValidationException)
            {
                var ex = (ErrorOnValidationException)context.Exception;

                var errorResponse = new ResponseErrorJson(ex.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new ObjectResult(errorResponse);
            }
        }
        private void ThrowUnkownError(ExceptionContext context) 
        {
            var errorResponse = new ResponseErrorJson("uknown Error");

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);

        }

    }
}
