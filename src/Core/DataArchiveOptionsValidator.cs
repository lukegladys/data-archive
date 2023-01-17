using ConfigureCount;
using FluentValidation;

namespace Core;

public class DataArchiveOptionsValidator : AbstractValidator<DataArchiveOptions>
{
    public DataArchiveOptionsValidator()
    {
        RuleFor(x => x.BatchSize).NotEmpty(); // not null
    }
}