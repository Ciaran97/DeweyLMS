using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeweyLMS.Models
{
    public class UserPoint
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public long TotalPoints { get; set; }
    }
}