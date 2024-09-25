using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Business.Model;

public partial class Exam
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public string ExamName { get; set; } = null!;

    public double? MinAccepted { get; set; }

    public double? MinValue { get; set; }

    public double? MaxValue { get; set; }

    public double Weight { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<EvaluatorExamValue> EvaluatorExamValues { get; set; } = new List<EvaluatorExamValue>();
}
