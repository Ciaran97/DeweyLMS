
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
        public ActionResult StartGame(ReOrderData collection)
        {
            List <SelectListItem> selection = Session["CallNumbers"] as List<SelectListItem>;

            ReOrderData reorder = new ReOrderData();


            string Option1 = selection[int.Parse(collection.Option1)].Text;

            string Option2 = selection[int.Parse(collection.Option2)].Text;

            string Option3 = selection[int.Parse(collection.Option3)].Text;

            string Option4 = selection[int.Parse(collection.Option4)].Text;

            string Option5 = selection[int.Parse(collection.Option5)].Text;

            string Option6 = selection[int.Parse(collection.Option6)].Text;

            string Option7 = selection[int.Parse(collection.Option7)].Text;

            string Option8 = selection[int.Parse(collection.Option8)].Text;

            string Option9 = selection[int.Parse(collection.Option9)].Text;

            string Option10 = selection[int.Parse(collection.Option10)].Text;



            string[] call1 = Option1.Split('.');

            string[] call2 = Option2.Split('.');

            string[] call3 = Option3.Split('.');

            string[] call4 = Option4.Split('.');

            string[] call5 = Option5.Split('.');

            string[] call6 = Option6.Split('.');

            string[] call7 = Option7.Split('.');

            string[] call8 = Option8.Split('.');

            string[] call9 = Option9.Split('.');

            string[] call10 = Option10.Split('.');

            List<int> userResults = new List<int>();

            userResults.Add(int.Parse(call1[0].ToString() + call1[1].ToString()));

            userResults.Add(int.Parse(call2[0].ToString() + call2[1].ToString()));

            userResults.Add(int.Parse(call3[0].ToString() + call3[1].ToString()));

            userResults.Add(int.Parse(call4[0].ToString() + call4[1].ToString()));

            userResults.Add(int.Parse(call5[0].ToString() + call5[1].ToString()));

            userResults.Add(int.Parse(call6[0].ToString() + call6[1].ToString()));

            userResults.Add(int.Parse(call7[0].ToString() + call7[1].ToString()));

            userResults.Add(int.Parse(call8[0].ToString() + call8[1].ToString()));

            userResults.Add(int.Parse(call9[0].ToString() + call9[1].ToString()));

            userResults.Add(int.Parse(call10[0].ToString() + call10[1].ToString()));

            List<int> SortedResult = new List<int>();

            foreach(SelectListItem item in selection)
            {
                string Text = item.Text;

                string[] numbers = Text.Split('.');

                SortedResult.Add(int.Parse(numbers[0].ToString() + numbers[1].ToString()));
            }

            SortedResult.Sort();
            reorder.IsBatchCorrect = true;

            for(int i = 0; i < 10; i++)
            {
                if(SortedResult[i] != userResults[i])
                {
                    reorder.IsBatchCorrect = false;

                }
               
            }

            int Attempt = int.Parse(Session["AttemptNumber"].ToString());

            if (reorder.IsBatchCorrect)
            {

                if(Attempt == 1)
                {
                    string userid = User.Identity.GetUserId();
                   UserPoint up = context.UserPoints.Where(a => a.UserId.Equals(userid)).FirstOrDefault();
                    reorder.PointAwarded = true;
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
                    Debug.WriteLine(User.Identity.GetUserId());
                    
                }

                return View("Index");
            }
            else
            {
               
                if(Attempt == 3)
                {

                   return RedirectToAction("StartGame");
                }
                else
                {
                Session["AttemptNumber"] = Attempt + 1;
                reorder.Options = selection;
                return View(reorder);
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
            reorder.IsBatchCorrect = true;

            return reorder;
        }     
    }
}
