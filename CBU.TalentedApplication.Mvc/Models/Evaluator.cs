using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBU.TalentedApplication.Mvc.Models;

public partial class Evaluator
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
