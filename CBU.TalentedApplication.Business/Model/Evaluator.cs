using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Business.Model;

public partial class Evaluator
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<EvaluatorExamValue> EvaluatorExamValues { get; set; } = new List<EvaluatorExamValue>();

    public virtual User User { get; set; } = null!;
}
