using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Mvc.Models;

public partial class Applicant
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BranchId { get; set; }

    public double? FinalScore { get; set; }

    public virtual ICollection<ApplicantCriteriaValue> ApplicantCriteriaValues { get; set; } = new List<ApplicantCriteriaValue>();

    public virtual ICollection<ApplicantDocument> ApplicantDocuments { get; set; } = new List<ApplicantDocument>();

    public virtual Branch Branch { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
