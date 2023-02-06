using CheekyB.Metadata;
using FluentValidation;

namespace CheekyB.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        List<string> ruleSets = null;
        var obj = context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T)) as T;

        if (obj is null)
        {
            return Results.BadRequest();
        }

        if (context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<RuleSetMetadata>() is { } meta)
        {
            ruleSets = meta.RuleSets;
        }

        var validationResult = ruleSets is { Count: > 0 }
            ? await _validator.ValidateAsync(obj, options =>
            {
                if (ruleSets != null)
                {
                    options.IncludeRuleSets(string.Join(",", ruleSets)).IncludeAllRuleSets();
                }
            })
            : await _validator.ValidateAsync(obj);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(string.Join("/n", validationResult.Errors));
        }

        return await next(context);
    }
}