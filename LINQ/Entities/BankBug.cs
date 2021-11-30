using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Entities
{
    /// <summary>
    /// Сущность ошибки банкомата
    /// </summary>

    public class BankBug
    {
        public string Key { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public string Assignee { get; set; }
        public string Labels { get; set; }
        public string FixVersion { get; set; }
        public string Reporter { get; set; }
        public string IssueType { get; set; }
        public string OriginalEstimate { get; set; }
        public string Priority { get; set; }
        public string Sprint { get; set; }
        public string DueDate { get; set; }
        public string Created { get; set; }
        public string QADueDate { get; set; }
    }    
}
