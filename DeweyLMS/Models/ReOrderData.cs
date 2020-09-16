using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeweyLMS.Models
{
    public class ReOrderData
    {

        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string Option6 { get; set; }
        public string Option7 { get; set; }
        public string Option8 { get; set; }
        public string Option9 { get; set; }
        public string Option10 { get; set; }
        public IEnumerable<SelectListItem> Options { get; set; }

        public Boolean IsBatchCorrect { get; set; }

        public Boolean PointAwarded { get; set; }

    }
}