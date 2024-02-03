namespace MarketPlace.Domain.Entitites.Users;

public class Seller : ApplicationUser
{
    public string CompanyName { get; set; } = default!;
    public string FullCompanyName { get; set; } = default!;
    public string CommonInformation { get; set; } = default!;
    public string Bank { get; set; } = default!;
    public string IBAN { get; set; } = default!;

    public virtual ICollection<Product>? Products { get; set; }
}
