using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enteripse_web.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime closureDate     { get; set; }
        public DateTime FinalDate { get; set; }
    }
}