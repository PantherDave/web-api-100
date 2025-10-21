using FluentValidation;

namespace SoftwareCenterApi.Vendors.Models;

public record VendorPointOfContact
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class VendorPointOfContactlValidator : AbstractValidator<VendorPointOfContact>
{
    public VendorPointOfContactlValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
        RuleFor(v => v.Email).NotEmpty().When(v => v.Phone == "");
        RuleFor(v => v.Phone).NotEmpty().When(v => v.Email == "");
    }
}