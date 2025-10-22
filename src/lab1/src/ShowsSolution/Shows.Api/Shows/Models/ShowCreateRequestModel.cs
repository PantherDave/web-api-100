using FluentValidation;

namespace Shows.Api.Shows.Models;

public record ShowCreateRequestModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService {  get; set; } = string.Empty;
}

public class ShowCreateRequestValidator : AbstractValidator<ShowCreateRequestModel> 
{
    public ShowCreateRequestValidator()
    {
        RuleFor(s => s.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(s => s.Description).MinimumLength(10).MaximumLength(100);
        RuleFor(s => s.StreamingService).NotEmpty();
    }
}
