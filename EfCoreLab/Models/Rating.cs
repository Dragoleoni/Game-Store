using System;
using System.Collections.Generic;

namespace EfCoreLab.Models;

public partial class Rating
{
    public int GameId { get; set; }

    public int UserId { get; set; }

    public int? Rating1 { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
