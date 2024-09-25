using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Business.Model;

public partial class Document
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public string DocumentName { get; set; } = null!;

    public int Required { get; set; }

    public virtual ICollection<ApplicantDocument> ApplicantDocuments { get; set; } = new List<ApplicantDocument>();

    public virtual Branch Branch { get; set; } = null!;
}
