using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enteripse_web.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        
        [Display(Name="Department Name")]
        public string Name { get; set; }
    }
}