using CBU.TalentedApplication.Business.Repo;
using CBU.TalentedApplication.Business.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Collections.Generic;
using CBU.TalentedApplication.Business;
using Microsoft.CodeAnalysis.Operations;
using CBU.TalentedApplication.Business.Model.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace CBU.TalentedApplication.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!SessionIsValid()) 
            {
                return RedirectToAction("Index", "Account");
            }
            
            ViewBag.FullName = AuthenticatedUser.FullName;
            
            if(AuthenticatedUser.RoleId==3)
            {
                BranchApplicantList branchApplicantList = new BranchApplicantList();
                branchApplicantList.branches = new BranchRepo().Search(x => 1 == 1).ToList();
                List<Applicant> apps = new ApplicantRepo().Search(x => x.UserId == AuthenticatedUser.Id).ToList();

                foreach (Applicant a in apps)
                {
                    applicantReview review = new applicantReview();
                    review.BranchId = a.BranchId;
                    review.branchName = new BranchRepo().Search(x => x.Id == a.BranchId).Single().Name;
                    review.finalscore = a.FinalScore;
                    review.Id = a.Id;
                    branchApplicantList.applicants.Add(review);
                }

                return View(branchApplicantList);
            }
            else 
            {
                TempData["ErrorMessage"] = "This page can be accesseed only by students";
                return RedirectToAction("Index","Account"); 
            }
        }

        [HttpPost]
        public IActionResult Criterias(int branchId)
        {
            CriteriaRepo crRepo = new CriteriaRepo();
            List<GradedCriterion> model = crRepo.GetCriteria(branchId);
            
            ViewBag.FullName = AuthenticatedUser.FullName;

            return View(model);
        }

        [HttpGet]
        [Route("Home/Criterias/{applicant?}")]
        public IActionResult Criterias(Applicant applicant)
        {
            CriteriaRepo crRepo = new CriteriaRepo();
            List<GradedCriterion> model = crRepo.GetCriterias(applicant);
            ViewBag.FullName = AuthenticatedUser.FullName;

            return View(model);
        }
        [HttpPost]
        public IActionResult AddGrades(Grades[] Grades)
        {
            if (Grades == null || Grades.Length == 0)
                throw new Exception("Notlar girilmemiş");

            BranchRepo branchRepo = new BranchRepo();
            CriteriaRepo criteriaRepo = new CriteriaRepo();

            var ids = Grades.Select(x => x.CriterionId);
            var branchId = criteriaRepo.Search(x => ids.Contains(x.Id))
                                       .Select(x => x.BranchId)
                                       .Distinct()
                                       .SingleOrDefault();
            ApplicantRepo applicantRepo = new ApplicantRepo();
            var applicant = applicantRepo.Search(x => x.BranchId == branchId && x.UserId == AuthenticatedUser.Id).SingleOrDefault();
            bool existed = true;
            if (applicant == null)
            {
                existed = false;
                applicant = applicantRepo.Add(new Applicant
                {
                        
                    UserId = AuthenticatedUser.Id,
                    BranchId = branchId,
                });
            }
            Repo<ApplicantCriteriaValue> repo = new Repo<ApplicantCriteriaValue>();

            foreach (var g in Grades)
            {

                // Add the new value to the repository
                if (!existed)
                {
                    ApplicantCriteriaValue entry = new ApplicantCriteriaValue
                    {
                        CriteriaId = g.CriterionId,
                        Value /*class parametre*/= g.Grade,
                        ApplicantId = applicant.Id
                    };
                    repo.Add(entry);
                }
               
                else
                {
                    // Fetch the existing ApplicantCriteriaValue entity from the database
                    var current = repo.Search(x => x.ApplicantId == applicant.Id && x.CriteriaId == g.CriterionId).SingleOrDefault();

                    if (current != null)
                    {
                        // Modify the existing entity
                        current.Value = g.Grade;  // Update the grade value

                        // Call update with the modified entity
                        repo.Update(current);
                    }
                }

            }
            TempData["SuccessMessage"] = "bilgiler başariyla kaydedildi.";
            return RedirectToAction("AddDocuments", "Home", new { BranchId = branchId });
        }

        [HttpGet]
        [Route("Home/AddDocuments/{BranchId?}")]

        public IActionResult AddDocuments(int BranchId)
        {   
            DocumentRepo DcRepo = new DocumentRepo();
            List<Document> model = DcRepo.GetDocuments(BranchId);

            return View(model);
        }

        [HttpPost]
        public IActionResult AddDocuments(Files[] files)
        {
            foreach (var fileModel in files)
            {
                ApplicantDocumentRepo repo = new ApplicantDocumentRepo();

                if (fileModel.file != null && fileModel.file.Length > 0)
                {
                    // Generate a unique file name for the uploaded file
                    var fileName = "F_"+Guid.NewGuid().ToString("N")+ Path.GetExtension(fileModel.file.FileName);//Path.GetFileName(fileModel.file.FileName);

                    // Set the path where the file will be saved
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    // Save the file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        fileModel.file.CopyTo(stream);
                    }

                    ApplicantDocument applicantDocument = new ApplicantDocument();
                    applicantDocument.DocumentId = fileModel.DocumentId;
                    applicantDocument.ApplicantId = new ApplicantRepo().Search(x => x.UserId == AuthenticatedUser.Id && x.BranchId == 1).Single().Id;
                    applicantDocument.DocumentPath = filePath;
                    applicantDocument.DisplayName = fileModel.file.FileName;

                    repo.Add(applicantDocument);                   
                }
            }

            // Redirect or return the view after processing
            return RedirectToAction("Index", "Home"); // or return View() to reload the page
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult DeleteApplicant(int applicantId)
        {
            Applicant applicant = new Repo<Applicant>().Find(applicantId);
            // Find the applicant with the related entities
            if (applicant == null)
            {
                return NotFound();
            }
            // Remove related ApplicantCriteriaValues
            //if (applicant.ApplicantCriteriaValues.Any())
            if(new Repo<ApplicantCriteriaValue>().Search(x=>x.ApplicantId == applicantId).Any())
            {
                Repo<ApplicantCriteriaValue> repo = new Repo<ApplicantCriteriaValue>();
                foreach (var value in repo.Search(x=>x.ApplicantId==applicant.Id).ToList()) 
                { repo.Delete(value.Id); }
                
            }
            // Remove related ApplicantDocuments
            if (new Repo<ApplicantDocument>().Search(x => x.ApplicantId == applicantId).Any())
            {
                Repo<ApplicantDocument> repo = new Repo<ApplicantDocument>();

                foreach (var value in repo.Search(x => x.ApplicantId == applicant.Id).ToList())
                { repo.Delete(value.Id); }
            }
            // Remove related EvaluatorExamValues
            if (new Repo<EvaluatorExamValue>().Search(x => x.ApplicantId == applicantId).Any())
            {
                Repo<EvaluatorExamValue> repo = new Repo<EvaluatorExamValue>();

                foreach (var value in repo.Search(x => x.ApplicantId == applicant.Id).ToList())
                { repo.Delete(value.Id); }
            }
            // Finally, remove the applicant
            new Repo<Applicant>().Delete(applicant.Id);
            // Save changes to the database
            return RedirectToAction("Index","Home"); 
        }


    }
}
