using System;
using System.Collections.Generic;

namespace GameStoreLab.Domain.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? DeveloperId { get; set; }

    public int? ReleaseYear { get; set; }

    public decimal? Price { get; set; }

    public virtual Developer? Developer { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
