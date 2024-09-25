using CBU.TalentedApplication.Business.Model;
using CBU.TalentedApplication.Business.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Repo
{
    public class EvaluatorRepo : Repo<Evaluator>
    {

        public List<applicantReview> GetAllApplicants(int userId)//the user id of the person how is an evaluator in one or more branch
        {
            EvaluatorRepo evaluatorRepo = new EvaluatorRepo();
            List<applicantReview> AllApplicants = new List<applicantReview>();   

            if (userId == null) { return null; }
            List<Evaluator> AccessedBranches = evaluatorRepo.Search(x => x.UserId == userId).ToList();
            foreach (Evaluator AccessedBranch in AccessedBranches)
            {
                Branch myBranch = new BranchRepo().Search(x=>x.Id== AccessedBranch.BranchId).Single();
                List<Applicant> applicants = new Repo<Applicant>().Search(x=>x.BranchId== myBranch.Id).ToList();
                foreach (Applicant applicant in applicants)
                {
                    applicantReview YouAreMyEvaluator = new applicantReview();
                    YouAreMyEvaluator.ApplicantName =
                                      new Repo<User>().Search(x=>x.Id==applicant.UserId).Single().FullName;
                    YouAreMyEvaluator.Id = applicant.Id;
                    YouAreMyEvaluator.branchName = 
                                      new Repo<Branch>().Search(x => x.Id == applicant.BranchId).Single().Name;
                    YouAreMyEvaluator.finalscore = applicant.FinalScore;
                    YouAreMyEvaluator.BranchId = applicant.BranchId;
                    YouAreMyEvaluator.UserId = applicant.UserId;
                    AllApplicants.Add(YouAreMyEvaluator);
                }
            }
            return AllApplicants;

        }
    }
}
