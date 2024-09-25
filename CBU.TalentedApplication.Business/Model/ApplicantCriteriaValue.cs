using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Business.Model;

public partial class ApplicantCriteriaValue
{
    public int Id { get; set; }

    public int ApplicantId { get; set; }

    public int CriteriaId { get; set; }

    public double? Value { get; set; }

    public virtual Applicant Applicant { get; set; } = null!;

    public virtual Criterion Criteria { get; set; } = null!;
}
