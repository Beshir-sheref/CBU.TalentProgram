using CBU.TalentedApplication.Business;
using CBU.TalentedApplication.Business.Model;
using CBU.TalentedApplication.Business.Model.ViewModels;
using CBU.TalentedApplication.Business.Repo;
using Microsoft.AspNetCore.Mvc;

namespace CBU.TalentedApplication.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        protected TalenedSystemContext db = new TalenedSystemContext();
        /*
         * 1- Login
         * 2- LostPassword
         * 3- Kullanıcı kaydı
         * 4- Kullanıcı giriş yaptıktan sonra kullanıcının bilgileri session içerisinde tutulacak
         */
        private readonly UserRepo repo = new UserRepo();

        public IActionResult Index()
        {
            
            return View();
        }

        User _user=new User();


        [HttpPost]
        public IActionResult Login(string email,string password)
        {
            bool isValid= repo.ValidateUser(email,password);

            if (!isValid)
            {
                TempData["ErrorMessage"] = "Geçersiz kullanıcı bilgileri";

                return RedirectToAction("Index", "Account");
            }
            else
            {
                _user = repo.GetUserdata(email, password);
                
                AuthenticatedUser= _user;
                ViewBag.FullName= AuthenticatedUser.FullName;

                if (AuthenticatedUser.RoleId == 3)
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
                    return RedirectToAction("Index","Home", branchApplicantList);
                }
                else if (AuthenticatedUser.RoleId == 2)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (AuthenticatedUser.RoleId == 4)
                {
                    return RedirectToAction("GetApplicants", "Evaluator");
                }
                else
                {
                    TempData["ErrorMessage"] = "Role Id is not recognized";
                    return RedirectToAction("Index", "Account");
                }
            } 
        }
        [HttpGet]
        public IActionResult LostPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LostPassword(string email, string password, string repeated)
        {

            bool isValid = repo.ValidateEmail(email);
            if (!isValid)
            {
                TempData["ErrorMessage"] = "Email Bulunamadı, mail adresinizi kontrol ediniz";
                return RedirectToAction("LostPassword");
            }

            else if (password != repeated)
            {
                TempData["ErrorMessage"] = "Şifre ve şifre tekrarı alanlarındaki girdiler aynı olmalıdır. ";
                return RedirectToAction("LostPassword");
            }
            else
            {
                User UpdatedUser = new User();
                try
                {
                    UpdatedUser = repo.Search(x => x.Email == email).Single();
                    UpdatedUser.Password = ConvertToMD5(password);
                    repo.Update(UpdatedUser);
                    TempData["SuccessMessage"] = "şifre başarıyla değiştirildi.";
                    return RedirectToAction("Index", "Account");

                }
                catch
                {
                    return RedirectToAction("Index", "Account");
                }
                
            }
        }






        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public IActionResult SignUp(string email,string fullName,string password,string phone,string idNumber)
        {
            if (email == null) {
                TempData["ErrorMessage"] = "Email bilgisi doldurulmalı";
                return View();
            }

            if (fullName == null) {
                TempData["ErrorMessage"] = "ad soyad bilgisi doldurulmalı";
                return View();
            }

            if (password == null) {
                TempData["ErrorMessage"] = "şifre bilgisi girilmeli";
                return View();
            }

            if (phone == null) {
                TempData["ErrorMessage"] = "telefon bilgisi girilmeli";
                return View();
            }

            if (idNumber == null) {
                TempData["ErrorMessage"] = "tc numarası bilgisi girilmeli";
                return View();
            }

            if (!email.Contains("@"))
            {
                TempData["ErrorMessage"] = "Email, formata uygun değil";
                return RedirectToAction("SignUp");
            }

            bool isExisted = repo.ValidateEmail(email);
            if (isExisted)
            {
                TempData["ErrorMessage"] = "Email zaten kayıtlı";
                return View();
            }

            else {
                var AddeddUser = new User();

                    AddeddUser.Email = email;
                    AddeddUser.FullName = fullName;
                    AddeddUser.Password = ConvertToMD5(password);
                    AddeddUser.Phone = phone;   
                    AddeddUser.IdNumber = idNumber;
                    AddeddUser.RoleId = 3;
                    AddeddUser.Active = true;
                    AddeddUser.Role = null;
                
                    repo.Add(AddeddUser);
                    TempData["SuccessMessage"] = "Kullanıcı başariyla kaydedildi.";
                    return RedirectToAction("Index", "Home");


            }

        }

    }
}
