using System;
using System.Collections.Generic;

namespace Dal.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();
}
