using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Model.ViewModels
{
    public class BranchApplicantList
    {

        public List<Branch> branches = new List<Branch>();
        public List<applicantReview> applicants = new List<applicantReview>();
        public BranchApplicantList() { }

    }
}
