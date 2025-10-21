using SoftwareCenterApi.Vendors.Models;

namespace SoftwareCenterApi.Vendors.Entities;

// what I'm actually storing in the db
public class VendorEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();
    // who created this?
}
