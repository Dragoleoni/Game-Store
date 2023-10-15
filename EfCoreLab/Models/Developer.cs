using System;
using System.Collections.Generic;

namespace EfCoreLab.Models;

public partial class Developer
{
    public int DeveloperId { get; set; }

    public string? DeveloperName { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public override string ToString()
    {
        return "{ DeveloperId: " + DeveloperId + "; Name: " + DeveloperName + "}"; 
    }
}
