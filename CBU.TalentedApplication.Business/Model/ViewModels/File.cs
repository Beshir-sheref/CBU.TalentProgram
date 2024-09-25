using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CBU.TalentedApplication.Business.Model.ViewModels
{
    public class Files
    {
        public int DocumentId { get; set; }
        public IFormFile file { get; set; }  // File uploaded by the user, if any
        public Files()
        {
        }

    }
}
