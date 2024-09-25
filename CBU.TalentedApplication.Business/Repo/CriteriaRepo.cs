using CBU.TalentedApplication.Business.Model;
using CBU.TalentedApplication.Business.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Repo
{
    public class CriteriaRepo : Repo<Criterion>
    {
        public List<GradedCriterion> GetCriteria(int branchId)
        {
            if (branchId == null) { return null; }
            List<GradedCriterion> Graded = new List<GradedCriterion>();
            List<Criterion> liste = Search(x => x.BranchId == branchId).ToList();
            foreach (var criterion in liste)
            {
                GradedCriterion ob = new GradedCriterion();
                ob.BranchId = criterion.BranchId;
                ob.MaxValue = criterion.MaxValue;
                ob.MinValue = criterion.MinValue;
                ob.CriteriaName = criterion.CriteriaName;
                ob.Branch=criterion.Branch;
                ob.MinAccepted=criterion.MinAccepted;
                ob.Weight=criterion.Weight;
                ob.Id=criterion.Id;
                Graded.Add(ob);
            }

            
            return Graded;

        }
        public List<GradedCriterion> GetCriterias(Applicant applicant)
        {
            List<GradedCriterion> Graded = new List<GradedCriterion>();
            //int  RoleId = new UserRepo().Search(x => x.Id == applicant.UserId).Single().RoleId;
            List<Criterion> liste = Search(x => x.BranchId == applicant.BranchId).ToList();
            foreach (var criterion in liste)
            {
                GradedCriterion gradedCriterion = new GradedCriterion();
                gradedCriterion.BranchId = criterion.BranchId;
                gradedCriterion.MaxValue = criterion.MaxValue;
                gradedCriterion.MinValue = criterion.MinValue;
                gradedCriterion.CriteriaName = criterion.CriteriaName;
                gradedCriterion.Branch = criterion.Branch;
                gradedCriterion.MinAccepted = criterion.MinAccepted;
                gradedCriterion.Weight = criterion.Weight;
                gradedCriterion.Id = criterion.Id;
                ApplicantCriteriaValueRepo repo=new ApplicantCriteriaValueRepo();
                gradedCriterion.Grade = repo.Search(x=>x.ApplicantId==applicant.Id && x.CriteriaId== gradedCriterion.Id).Single().Value;
                Graded.Add(gradedCriterion);
            }

            if (applicant.BranchId == null) { return null; }

            return Graded;

        }
    }
}
