using System;
using System.Collections.Generic;

namespace EfCoreLab.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? GameId { get; set; }

    public int? BuyerId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public decimal? Price { get; set; }

    public virtual User? Buyer { get; set; }

    public virtual Game? Game { get; set; }
}
