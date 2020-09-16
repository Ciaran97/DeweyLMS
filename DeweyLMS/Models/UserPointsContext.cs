using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DeweyLMS.Models
{
    public class UserPointsContext : ApplicationDbContext
    {
        public DbSet<UserPoint> UserPoints { get; set; }
    }
}