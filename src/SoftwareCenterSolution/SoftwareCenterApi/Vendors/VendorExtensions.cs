using SoftwareCenterApi.Vendors.Models;

namespace SoftwareCenterApi.Vendors;

public static class VendorExtensions
{
    public static IServiceCollection AddVendorServices(this IServiceCollection services) 
    {
        services.AddScoped<VendorCreateModelValidator>();
        return services;
    }
}
