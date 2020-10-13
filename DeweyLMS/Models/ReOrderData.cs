using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeweyLMS.Models
{
    public class ReOrderData
    {


        public IEnumerable<SelectListItem> Options { get; set; }

        public IEnumerable<String> Results { get; set; }


        public Boolean IsBatchCorrect { get; set; }

        public Boolean PointAwarded { get; set; }

        public int Attempt { get; set; }

    }
}