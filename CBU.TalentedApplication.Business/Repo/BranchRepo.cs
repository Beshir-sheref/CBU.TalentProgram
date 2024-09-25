using CBU.TalentedApplication.Business.Model;
using CBU.TalentedApplication.Business.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CBU.TalentedApplication.Business
{
    public class BranchRepo : Repo<Branch>
    {
        public List<Branch> GetAll() {
            List<Branch> listeee = new List<Branch>();
            listeee = Search(x=>1==1);
            return listeee; 
        } 
    }
}
