using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Model.ViewModels
{
    public class EvaluateViewModel
    {
        public List<GradedExam> ExamValue = new List<GradedExam>();
        public string applicantName { get; set; }
        public string EvaluatorName { get; set; }
        public string BranchName { get; set; }

        public EvaluateViewModel() { }
    }
}
