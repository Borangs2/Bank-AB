namespace Bank_AB.ViewModels;


public class CustomerViewModel
{
    public int Id { get; set; }
    public string Givenname { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Telephone { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string City { get; set; } = null!;
}
