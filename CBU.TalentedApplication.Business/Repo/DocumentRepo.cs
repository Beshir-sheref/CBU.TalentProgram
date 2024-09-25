using CBU.TalentedApplication.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Repo
{
    public class DocumentRepo : Repo<Document>
    {
        public List<Document> GetDocuments(int branchId)
        {
            if (branchId == null) { return null; }

            return Search(x => x.BranchId == branchId).ToList();

        }
        public List<Document> GetAll()
        {

            return Search(x => true).ToList();

        }
    }
}
