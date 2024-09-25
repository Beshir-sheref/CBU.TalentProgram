using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Business.Model;

public partial class ApplicantDocument
{
    public int Id { get; set; }

    public int DocumentId { get; set; }

    public int ApplicantId { get; set; }

    public string DocumentPath { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public virtual Applicant Applicant { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
