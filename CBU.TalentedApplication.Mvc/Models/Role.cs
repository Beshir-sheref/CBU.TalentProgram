using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Mvc.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Criterion> Criteria { get; set; } = new List<Criterion>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
