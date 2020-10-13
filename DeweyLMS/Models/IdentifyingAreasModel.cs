using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeweyLMS.Models
{
    public class IdentifyingAreasModel
    {

        public List<string> CallNumbers { get; set; }
        public List<string> CallDefinitions { get; set; }

        public List<ReturnColumnMatch> Results { get; set; }

    }
}