using System;
using System.Collections.Generic;

namespace CBU.TalentedApplication.Business.Model;

public partial class Branch
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();

    public virtual ICollection<Criterion> Criteria { get; set; } = new List<Criterion>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Evaluator> Evaluators { get; set; } = new List<Evaluator>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
