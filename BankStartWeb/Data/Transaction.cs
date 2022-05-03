﻿using System.ComponentModel.DataAnnotations;

namespace Bank_AB.Data;

public class Transaction
{
    public int Id { get; set; }

    [MaxLength(10)] public string Type { get; set; } = null!;

    [MaxLength(50)] public string Operation { get; set; } = null!;

    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public decimal NewBalance { get; set; }
}