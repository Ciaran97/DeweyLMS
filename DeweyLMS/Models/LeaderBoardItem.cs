using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DeweyLMS.Models
{
    public class LeaderBoardItem
    {
        [DisplayName("User Email")]
        public string UserEmail { get; set; }
        [DisplayName("Total Points")]
        public int TotalPoints { get; set; }



    }
}