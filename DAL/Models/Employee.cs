using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal EmployeeSalary { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeEmail { get; set; }
        public DateTime EmployeeJoiningDate { get; set; }
        public string EmployeeStatus { get; set; }
    }
}
