using Microsoft.Ajax.Utilities;
using DeweyLMS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeweyLMS.Controllers
{
    public class ReplacingBooksController : Controller
    {

        UserPointsContext context = new UserPointsContext();



        // GET: ReplacingBooks
        public ActionResult Index()
        {


            return View();
        }

        [Authorize]
        public ActionResult StartGame()
        {
            Session["AttemptNumber"] = 1;
            ReOrderData data = GetRandomCallsigns();


            Session["CallNumbers"] = data.Options.ToList();
            return View(data);
        }

        [HttpPost]
        public ActionResult StartGame(ReOrderData newdata)
        {
            List<SelectListItem> selection = Session["CallNumbers"] as List<SelectListItem>;
            List<int> userResults = new List<int>();
            List<int> sortedList = new List<int>();

              int Attempt = int.Parse(Session["AttemptNumber"].ToString());


            ReOrderData reorder = new ReOrderData();

            reorder.Options = selection;



            for (int i = 0; i < 10; i++)
            {
                List<String> Provided = new List<String>();
                List<String> UserSelected = new List<String>();

                Provided = selection[i].Text.Split('.').ToList();
                UserSelected = selection[int.Parse(newdata.Results.ToList().ElementAt(i))].Text.Split('.').ToList();

                String generatedValue = Provided[0] + Provided[1];
                String selectedValue = UserSelected[0] + UserSelected[1];

                userResults.Add(int.Parse(selectedValue));
                sortedList.Add(int.Parse(generatedValue));
                Debug.WriteLine(selectedValue);

            }

            sortedList.Sort();
            reorder.IsBatchCorrect = true;

            for (int i = 0; i < 10; i++)
            {
                if (sortedList[i] != userResults[i])
                {
                    reorder.IsBatchCorrect = false;
                    Debug.WriteLine("WRONG");

                }

            }

            if (reorder.IsBatchCorrect)
            {
                Session["AttemptNumber"] = 1;
                if (Attempt == 1)
                {
                    string userid = User.Identity.GetUserId();
                    UserPoint up = context.UserPoints.Where(a => a.UserId.Equals(userid)).FirstOrDefault();
                    reorder.PointAwarded = true;
                    Debug.WriteLine("Point Awarded");

                    if (up != null)
                    {
                        up.TotalPoints = int.Parse(up.TotalPoints.ToString()) + 1;
                        context.SaveChanges();
                    }
                    else
                    {
                        UserPoint point = new UserPoint();

                        point.UserId = userid;
                        point.TotalPoints = 1;

                        context.UserPoints.Add(point);
                        context.SaveChanges();
                    }
                    var details = new { Attempt = Attempt, Correct = false, Reload = false, PointAwarded = true };
                    
                    return Json(details);
                }
                    
                var test = new { Attempt = Attempt, Correct = true, Reload = false, PointAwarded = false };
                    
                    return Json(test);
            } else
            {

                if (Attempt == 3)
                {
                    var test = new { Attempt = 1, Correct = false, Reload = true, PointAwarded = false };
                    return Json(test);
                }
                else
                {
                    Attempt++;
                    Session["AttemptNumber"] = Attempt;
                    reorder.Options = selection;
                    var test = new { Attempt = Attempt, Correct = false, Reload = false, PointAwarded = false };
                    
                    return Json(test);
                }
            }

        }



 
        public ReOrderData GetRandomCallsigns()
        {
            Random _random = new Random();
            List<SelectListItem> CallNumbers = new List<SelectListItem>();

            for(int i = 0; i < 10; i++)
            {
                string callNum = _random.Next(999).ToString().PadLeft(3, '0') + "." + _random.Next(999).ToString().PadLeft(3, '0');
                CallNumbers.Add(new SelectListItem { Text = callNum, Value = i.ToString()});
            }

            ReOrderData reorder = new ReOrderData();

            reorder.Options = CallNumbers;
            reorder.IsBatchCorrect = false;
            reorder.Attempt = 1;

            return reorder;
        }     
    }
}


