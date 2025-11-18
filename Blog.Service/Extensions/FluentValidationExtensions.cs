using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Blog.Service.Extensions
{
    public static class FluentValidationExtensions
    {
        // Buraya FluentValidation ile ilgili extension metotlar eklenebilir.
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
