namespace AccountingDashboard.Infrastructure;

using FluentValidation;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class ValidationActionFilter(IEnumerable<IValidator> validators) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var validatedParameter = context.ActionDescriptor.Parameters
            .FirstOrDefault(x => x.BindingInfo?.BindingSource == BindingSource.Body || x.BindingInfo?.BindingSource == BindingSource.Form);
        if (validatedParameter is null)
        {
            await next();
            return;
        }

        var typeValidators = validators.Where(x => x.CanValidateInstancesOfType(validatedParameter.ParameterType));

        foreach (var validator in typeValidators)
        {
            var result = await validator.ValidateAsync(new ValidationContext<object>(context.ActionArguments[validatedParameter.Name]!));
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

        await next();
    }
}
