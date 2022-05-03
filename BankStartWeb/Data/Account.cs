using System.ComponentModel.DataAnnotations;

namespace Bank_AB.Data;

public class Account
{
    public int Id { get; set; }


    [MaxLength(10)] public string AccountType { get; set; } = null!;

    public DateTime Created { get; set; }
    public decimal Balance { get; set; }

    public List<Transaction> Transactions { get; set; } = new();
}