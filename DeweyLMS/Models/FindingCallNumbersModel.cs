using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeweyLMS.Models
{
    public class FindingCallNumbersModel
    {

       public string ThirdLevelKey { get; set; }
        public string ThirdLevelDescription { get; set; }

        public List<string> Options { get; set; }

        public string result { get; set; }


    }
}