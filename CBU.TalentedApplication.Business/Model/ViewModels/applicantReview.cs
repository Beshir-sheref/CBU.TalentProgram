using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Model.ViewModels
{
    public class applicantReview
    {
        public int? Id { get; set; }
        public int UserId {  get; set; }
        public string branchName { get; set; }
        public string ApplicantName { get; set; }
        public double? finalscore { get; set; }
        public int BranchId { get; set; }

        public applicantReview() { }
    }
}
