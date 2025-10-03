using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairOfEmployees.Domain.Models
{
    public sealed class PairOfEmployeesData
    {  
        public int Id { get; set; }

        public int EmpID { get; set; }
        public int ProjectID { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }

        private PairOfEmployeesData() {}

        public PairOfEmployeesData(int EmpID, int ProjectID, string FileName, DateOnly DateFrom, DateOnly DateTo)
        {
            this.EmpID = EmpID;
            this.ProjectID = ProjectID;
            this.FileName = FileName ?? string.Empty;
            this.DateFrom = DateFrom;
            this.DateTo = DateTo;
        }
    }
}
