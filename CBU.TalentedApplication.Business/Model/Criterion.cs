using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Business.Model;

public partial class Criterion
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public string CriteriaName { get; set; } = null!;

    public double? MinAccepted { get; set; }

    public double? MinValue { get; set; }

    public double MaxValue { get; set; }

    public double Weight { get; set; }

    public virtual ICollection<ApplicantCriteriaValue> ApplicantCriteriaValues { get; set; } = new List<ApplicantCriteriaValue>();

    public virtual Branch Branch { get; set; } = null!;
}
