using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumulative_Part_3.Models
{
    public class Teacher
    {
        //The following fields define an Teacher
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string EmpNum { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        //parameter-less constructor function
        public Teacher() { }

    }
}