namespace Bank_AB.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Operation { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
    }
}
