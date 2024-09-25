using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Model.ViewModels
{
    public partial class GradedExam
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamId { get; set; }
        public int ApplicantId { get; set; }
        public int EvaluatorId { get; set; }
        public int BranchId { get; set; }

        public string ExamName { get; set; } = null!;

        public double? MinAccepted { get; set; }
        public double? Value { get; set; }

        public double? MinValue { get; set; }

        public double MaxValue { get; set; }


        public double Weight { get; set; }

        public virtual ICollection<EvaluatorExamValue> ApplicantCriteriaValues { get; set; } = new List<EvaluatorExamValue>();

        public virtual Branch Branch { get; set; } = null!;

    }

}
