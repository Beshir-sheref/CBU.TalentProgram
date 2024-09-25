using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Mvc.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public bool? Active { get; set; }

    public string? Phone { get; set; }

    public string? IdNumber { get; set; }

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();

    public virtual ICollection<Evaluator> Evaluators { get; set; } = new List<Evaluator>();

    public virtual Role? Role { get; set; }
}
