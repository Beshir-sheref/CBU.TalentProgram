using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Model.ViewModels
{
    public class BranchCriteria
    {
        public List<Branch> branches = new List<Branch>();
        public Criterion criteria = new Criterion();
        public BranchCriteria() { }


    }
}
