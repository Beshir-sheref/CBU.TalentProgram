using CBU.TalentedApplication.Business.Repo;
using CBU.TalentedApplication.Business.Model;
using CBU.TalentedApplication.Business;
using Microsoft.CodeAnalysis.Operations;
using CBU.TalentedApplication.Business.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CBU.TalentedApplication.Mvc.Controllers
{
    public class EvaluatorController : BaseController
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Evaluate(Applicant App)
        {
            //get needed exams list
              //applicant.branch =>branch.exams
            List<Exam> exams = new List<Exam>();
            exams = new Repo<Exam>().Search(x=>x.BranchId == App.BranchId).ToList();
            List<GradedExam> examValue = new List<GradedExam>(); 
            foreach (Exam exam in exams)
            {
                GradedExam valued = new GradedExam();
                valued.ExamName = exam.ExamName;
                valued.ApplicantId = App.Id;
                valued.ExamId = exam.Id;
                valued.EvaluatorId = new EvaluatorRepo().
                        Search(x=>x.UserId==AuthenticatedUser.Id && x.BranchId ==App.BranchId).Single().Id;
                valued.BranchId = App.BranchId;
                try
                {valued.Value = new Repo<EvaluatorExamValue>().Search(x=>x.ApplicantId==App.Id && x.ExamId== exam.Id).Single().Value;}
                catch (Exception ex)
                {
                    valued.Value = null;
                }
                examValue.Add(valued);
            }
            EvaluateViewModel model = new EvaluateViewModel()
            {
                ExamValue = examValue,
                applicantName = new UserRepo().Find(App.UserId).FullName,
                EvaluatorName= AuthenticatedUser.FullName,
                BranchName=new Repo<Branch>().Search(x=>x.Id==App.BranchId).Single().Name,
            }; return View(model);
        }

        [HttpPost]
        public IActionResult Evaluate(Grades[] Grades,int ApplicantId, int BranchId)
        {
            if (Grades == null || Grades.Length == 0)
                throw new Exception("Notlar girilmemiş");

            Repo<EvaluatorExamValue> repo = new Repo<EvaluatorExamValue>();
            int EvaluatorId = new Repo<Evaluator>().Search(x => x.UserId == AuthenticatedUser.Id && x.BranchId == BranchId).Single().Id;

            foreach (var g in Grades)
            {
                bool existed = new Repo<EvaluatorExamValue>().Search(x=>x.ApplicantId== ApplicantId &&x.ExamId==g.ExamId).Any();
                // Add the new value to the repository
                if (!existed)
                {
                    EvaluatorExamValue entry = new EvaluatorExamValue
                    {
                        ExamId = g.ExamId,
                        Value /*class parametre*/= g.Grade,
                        ApplicantId = ApplicantId,
                        EvaluatorId = EvaluatorId
                    };
                    repo.Add(entry);
                }
                else
                {
                    // Fetch the existing ApplicantCriteriaValue entity from the database
                    var current = repo.Search(x => x.ApplicantId == ApplicantId && x.ExamId == g.ExamId).SingleOrDefault();
                    if (current != null)
                    {
                        // Modify the existing entity
                        current.Value = g.Grade;  // Update the grade value

                        // Call update with the modified entity
                        repo.Update(current);
                    }
                }
            }
            Repo<EvaluatorExamValue> values = new Repo<EvaluatorExamValue>();
            Repo<ApplicantCriteriaValue> Criteria = new Repo<ApplicantCriteriaValue>();

            List<EvaluatorExamValue>ListOfGrades=values.Search(x => x.ApplicantId==ApplicantId && x.EvaluatorId==EvaluatorId).ToList();

            List<ApplicantCriteriaValue> ListOfCriteria = Criteria.Search(x => x.ApplicantId == ApplicantId).ToList();
            //All grades and criterias are already inserted
            if (ListOfGrades.All(x => x != null)&& ListOfCriteria.All(x => x != null))
            {
                Repo<Applicant> Applicantrepo = new Repo<Applicant>();
                Applicant ToBeUpdated = Applicantrepo.Find(ApplicantId);
                if (ToBeUpdated != null)
                {
                    Double? finalScore =0;
                    foreach(EvaluatorExamValue v in ListOfGrades)
                    {
                        if (v.Value != null)
                        {
                            Double weight = new Repo<Exam>().Find(v.ExamId).Weight;
                            finalScore += (v.Value * weight/100);
                        }
                    }
                    foreach (ApplicantCriteriaValue v in ListOfCriteria)
                    {
                        if (v.Value != null)
                        {
                            Double weight = new Repo<Criterion>().Find(v.CriteriaId).Weight;
                            finalScore += (v.Value * weight / 100);
                        }
                    }

                    ToBeUpdated.FinalScore = finalScore;
                    Applicantrepo.Update(ToBeUpdated);       
                }
                
            }
            TempData["SuccessMessage"] = "bilgiler başariyla kaydedildi.";
            return RedirectToAction("GetApplicants", "Evaluator");
        }

        public IActionResult GetApplicants()
        {
            EvaluatorRepo repo = new EvaluatorRepo();
            List<applicantReview> model = repo.GetAllApplicants(AuthenticatedUser.Id);
            
            return View("_GetApplicants", model);
        }
    }
}
