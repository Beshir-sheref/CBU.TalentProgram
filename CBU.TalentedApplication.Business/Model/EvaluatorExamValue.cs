using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Business.Model;

public partial class EvaluatorExamValue
{
    public int Id { get; set; }

    public int ApplicantId { get; set; }

    public int EvaluatorId { get; set; }

    public int ExamId { get; set; }

    public double? Value { get; set; }

    public virtual Applicant Applicant { get; set; } = null!;

    public virtual Evaluator Evaluator { get; set; } = null!;

    public virtual Exam Exam { get; set; } = null!;
}
