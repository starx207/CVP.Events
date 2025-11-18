using System;
using System.ComponentModel.DataAnnotations;

namespace CVP.Events.Validation;

public abstract class DateRangeAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateRangeAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected abstract bool AreDatesOrdered(DateTime value, DateTime comparisonValue);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime)
        {
            return ValidationResult.Success;
        }

        var currentValue = (DateTime)value;
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property is null)
        {
            return new ValidationResult($"Unknown property: {_comparisonProperty}");
        }

        var comparisonValue = property.GetValue(validationContext.ObjectInstance);

        if (comparisonValue is not DateTime)
        {
            return ValidationResult.Success;
        }

        if (!AreDatesOrdered(currentValue, (DateTime)comparisonValue))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}

public class DateBeforeAttribute : DateRangeAttribute
{
    public DateBeforeAttribute(string comparisonProperty)
        : base(comparisonProperty)
        => ErrorMessage = "Date must be before comparison date";

    protected override bool AreDatesOrdered(DateTime value, DateTime comparisonValue) => value < comparisonValue;
}

public class DateAfterAttribute : DateRangeAttribute
{
    public DateAfterAttribute(string comparisonProperty)
        : base(comparisonProperty)
        => ErrorMessage = "Date must be after comparison date";

    protected override bool AreDatesOrdered(DateTime value, DateTime comparisonValue) => value > comparisonValue;
}
