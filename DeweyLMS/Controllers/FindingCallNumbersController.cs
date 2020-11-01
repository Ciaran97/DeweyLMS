using DeweyLMS.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeweyLMS.Controllers
{
    public class FindingCallNumbersController : Controller
    {

        UserPointsContext context = new UserPointsContext();


        //IDictionary<int, string> CallNumbers = new IDictionary<int, string>();

        public IDictionary<int, string> CallNumbers = new Dictionary<int, string>();



        // GET: FindingCallNumbers
        public ActionResult Index()
        {
            LoadDictionary();
            FindingCallNumbersModel FindCallNumbers = new FindingCallNumbersModel();
            List<string> Options = new List<string>();
            Random _random = new Random(); 
            string ThirdLevelDescription;
            string ThirdLevelKey = _random.Next(999).ToString().PadLeft(3, '0');

            // string callNum;


           

            CallNumbers.TryGetValue(int.Parse(ThirdLevelKey), out ThirdLevelDescription);
            string correctKey = ThirdLevelKey.Substring(0, 1).PadRight(3, '0');
            string correctDescription;
            CallNumbers.TryGetValue(int.Parse(correctKey), out correctDescription);
            Options.Add(correctKey + " " + correctDescription);
            while(Options.Count() < 4)
            {
                string Desc;
                string Key = _random.Next(9).ToString().PadRight(3, '0');
                CallNumbers.TryGetValue(int.Parse(Key), out Desc);
                string CallAdd = Key + " " + Desc;
                if (!Options.Contains(CallAdd))
                {
                    Options.Add(CallAdd);
                }
                
            }
            Options.Sort();

            FindCallNumbers.Options = Options;
            FindCallNumbers.ThirdLevelKey = ThirdLevelKey;
            FindCallNumbers.ThirdLevelDescription = ThirdLevelDescription;

            return View(FindCallNumbers);
        }

        public IDictionary<int, string> LoadDictionary()
        {
           // IDictionary<int, string> callNumbers = new Dictionary<int, string>();

        string fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/PROG_POE_Call_List.txt")).Replace("\n", "").Replace("\r", "");


            string[] words = fileContents.Split(';');
            foreach (string word in words)
            {
                string[] calls = word.Split(':');
                if (!word.Equals(""))
                {
                  
                    if (!calls[1].Equals("[Unassigned]"))
                    {
                        CallNumbers.Add(int.Parse(calls[0]), calls[1]);

                    }


                }


                
            }
            
            return CallNumbers;
        }



        [HttpPost]
        public ActionResult ReturnResults(string correct, string selected)
        {
            String successMessage;
            //JObject json = JObject.Parse(data);

            
            
            string correctDigit = correct.Substring(0, 1);
            string resultDigit = selected.Substring(0, 1);


            if (correctDigit.Equals(resultDigit))
            {
                string userid = User.Identity.GetUserId();
                UserPoint up = context.UserPoints.Where(a => a.UserId.Equals(userid)).FirstOrDefault();

                if (up == null)
                {
                    UserPoint point = new UserPoint();

                    up.UserId = userid;
                    up.TotalPoints = 1;

                    context.UserPoints.Add(up);
                    context.SaveChanges();
                }
                else
                {
                    up.TotalPoints = int.Parse(up.TotalPoints.ToString()) + 1;
                    context.SaveChanges();
                }
                successMessage = "correct";
            }
            else
            {
                successMessage = "incorrect";
            }
            
            return Json(successMessage);
        }



    }
}