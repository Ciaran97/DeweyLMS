using DeweyLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace DeweyLMS.Controllers
{
    public class HomeController : Controller
    {

        UserPointsContext context = new UserPointsContext();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult LeaderBoard()
        {

            List<UserPoint> UsersList = context.UserPoints.OrderByDescending(a => a.TotalPoints).ToList();
            

            List<LeaderBoardItem> LeaderBoardList = new List<LeaderBoardItem>();

            foreach(UserPoint item in UsersList)
            {
                LeaderBoardList.Add(new LeaderBoardItem
                {
                    UserEmail = context.Users.Where(a => a.Id.Equals(item.UserId)).Select(a => a.Email).FirstOrDefault(),
                    TotalPoints = int.Parse(item.TotalPoints.ToString())
                });
                
            }

            return View(LeaderBoardList);
        }

    }
}