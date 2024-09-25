using CBU.TalentedApplication.Business;
using CBU.TalentedApplication.Business.Model;
using CBU.TalentedApplication.Business.Model.ViewModels;
using CBU.TalentedApplication.Business.Repo;
using Microsoft.AspNetCore.Mvc;

namespace CBU.TalentedApplication.Mvc.Controllers
{
    public class AdminController1 : BaseController
    {
        
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddCriteria()
        {
            BranchCriteria model = GetModel();
            return PartialView("_AddCriteria", model);
        }
        [HttpPost]
        public IActionResult AddCriteria(Criterion criteria)
        {
            Criterion NewCriteria = new Criterion();
            NewCriteria.CriteriaName = criteria.CriteriaName;
            NewCriteria.MinAccepted = criteria.MinAccepted;
            NewCriteria.MinValue = criteria.MinValue;
            NewCriteria.MaxValue = criteria.MaxValue;
            NewCriteria.Weight = criteria.Weight;
            NewCriteria.BranchId = criteria.BranchId;

            CriteriaRepo repo = new CriteriaRepo();
            repo.Add(NewCriteria);
            return View("Index");
        }


        [HttpGet]
        public IActionResult AddExam()
        {
            BranchExam model = GetExamModel();
            return PartialView("_AddExam", model);
        }
        [HttpPost]
        public IActionResult AddExam(Exam exam)
        {
            Exam NewExam = new Exam();
            NewExam.ExamName = exam.ExamName;
            NewExam.MinAccepted = exam.MinAccepted;
            NewExam.MinValue = exam.MinValue;
            NewExam.MaxValue = exam.MaxValue;
            NewExam.Weight = exam.Weight;
            NewExam.BranchId = exam.BranchId;

            Repo<Exam> repo = new Repo<Exam>();
            repo.Add(NewExam);
            return View("Index");
        }

        public IActionResult GetExams()
        {
            return PartialView("_GetExams", new Repo<Exam>().Search(x => 1 == 1).ToList());
        }


        [HttpGet]
        public IActionResult AddDocument()
        {
            List<Branch> model =new BranchRepo().GetAll();
            return PartialView("_AddDocument" , model);
        }
        [HttpPost]
        public IActionResult AddDocument(Document document)
        {
            Document NewDocument = new Document();
            NewDocument.DocumentName = document.DocumentName;
            NewDocument.BranchId = document.BranchId;
            NewDocument.Required = document.Required;


            DocumentRepo repo = new DocumentRepo();
            repo.Add(NewDocument);
            TempData["SuccessMessage"] = "doküman tibi başarıyla eklendi";
            return View("Index");
        }
        public IActionResult GetDocuments()
        {
            return PartialView("_GetDocuments", new DocumentRepo().GetAll());
        }

        public IActionResult CriteriaPage()
        {
            BranchCriteria model = GetModel();
            return PartialView(model);
        }
        public IActionResult BranchPage()
        {
            return PartialView();
        }
        public IActionResult DocumentPage()
        {
            return PartialView();
        }
        private static BranchExam GetExamModel()
        {
            BranchRepo repo = new BranchRepo();
            BranchExam model = new BranchExam();
            model.branches = repo.GetAll();
            model.exam = new Exam();
            return model;
        }

        public IActionResult ExamPage()
        {
            BranchExam model = GetExamModel();
            return PartialView(model);
        }
        private static BranchCriteria GetModel()
        {
            BranchRepo repo = new BranchRepo();
            BranchCriteria model = new BranchCriteria();
            model.branches = repo.GetAll();
            model.criteria = new Criterion();
            return model;
        }
       

        [HttpGet]
        public IActionResult AddBranch()
        {

            BranchRepo repo = new BranchRepo();
            
            List<Branch> model = repo.GetAll();
            return PartialView("_AddBranch", model);
        }

        [HttpPost]
        public IActionResult AddBranch(Branch branch)
        {
            if (ModelState.IsValid)
            {
                Branch Newbranch = new Branch
                {
                    Name = branch.Name
                };

                BranchRepo repo = new BranchRepo();
                repo.Add(Newbranch);

                // Return a success response to indicate the branch was added
                return Json(new { success = true });
            }
            // If the model state is invalid, return the form again
            return View("BranchPage");
        }
        public IActionResult GetBranches()
        {
            return PartialView("_GetBranches", new BranchRepo().Search(x => 1 == 1));
        }

        public IActionResult GetCriterias()
        {
            return PartialView("_GetCriterias",new CriteriaRepo().Search(x=>1==1));
        }

        public IActionResult Branches()
        {
            BranchRepo repo = new BranchRepo();
            List<Branch> model = repo.GetAll();
            return PartialView("_Branches", model);
        }
    }
}
