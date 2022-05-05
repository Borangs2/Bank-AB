namespace Bank_AB.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string AccountType { get; set; } = null!;

        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
    }
}
