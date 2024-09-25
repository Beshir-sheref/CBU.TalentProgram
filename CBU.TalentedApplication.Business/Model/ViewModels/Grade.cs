using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Model.ViewModels
{
    public class Grades
    {
        public int CriterionId { get; set; }
        public int ExamId { get; set; }
        public double? Grade { get; set; }
        public Grades()
        {
        }
    }
}
