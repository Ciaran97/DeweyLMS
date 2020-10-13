using DeweyLMS.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeweyLMS.Controllers
{
    public class IdentifingAreasController : Controller
    {

        IDictionary<int, string> CallNumbers = LoadDictionary();

        UserPointsContext context = new UserPointsContext();

        // GET: IdentifingAreas
        public ActionResult Index()
        {

            IdentifyingAreasModel areas = new IdentifyingAreasModel();

            Random _random = new Random();

            int type = 1;/*_random.Next(2);*/

            if(type == 1)
            {
                List<string> callList = new List<string>();
                List<string> callDef = new List<string>();
                List<int> callDigits = new List<int>();
                while(callList.Count < 7)
                {
                    int callDigit = _random.Next(999);
                    string callNum = callDigit.ToString().PadLeft(3, '0');

             
                    
                    if(callDigit < 10 && !callList.Contains(callNum) && callDigits.Contains(callDigit))
                    {

                        callDigits.Add(callDigit);
                        string value = "";
                        CallNumbers.TryGetValue(callDigit, out value);
                        callList.Add(callNum);
                        callDef.Add(value);
                        Debug.WriteLine(value);
                    }
                    else
                    {
                        /*
                        int FirstDigit = callDigit;
                        while (FirstDigit >= 10)
                        {
                            FirstDigit = (FirstDigit - (FirstDigit % 10)) / 10;
                        }
                        */

                        int FirstDigit = int.Parse(callNum.Substring(0, 1));
                       if(!callDigits.Contains(FirstDigit))
                        {
                            callDigits.Add(FirstDigit);
                            string value = "";
                            CallNumbers.TryGetValue(FirstDigit, out value);
                            callList.Add(callNum);
                            callDef.Add(value);
                            Debug.WriteLine(value);

                        }


                    }
                    
                }
                callDef.Sort();

                foreach(string str in callDef)
                {
                    Debug.WriteLine(str);

                }


                areas.CallDefinitions = callDef;
                areas.CallNumbers = callList;
            }

            

            

            return View(areas);
        }

        [HttpPost]
        public ActionResult ReturnResults(List<ReturnColumnMatch> results)
        {
            String successMessage;
            int correct = 0;
            foreach(ReturnColumnMatch value in results)
            {
                int FirstDigit = int.Parse(value.callNumber.ToString());
                while (FirstDigit >= 10)
                {
                    FirstDigit = (FirstDigit - (FirstDigit % 10)) / 10;
                }
                string definition = "";
                CallNumbers.TryGetValue(FirstDigit, out definition);
                if (definition.Equals(value.Definition))
                {
                    Debug.WriteLine("Correct!!");
                    correct++;
                }
                else
                {
                    Debug.WriteLine("Youre a Failure!!");

                }
                Debug.WriteLine("Definition: " + value.Definition + ", CallNumber: " + value.callNumber);

            }

            if(correct == 4)
            {
                string userid = User.Identity.GetUserId();
                UserPoint up = context.UserPoints.Where(a => a.UserId.Equals(userid)).FirstOrDefault();

                if(up == null)
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
                successMessage = "Well Done, You correctly matched all 4 columns. You have been awarded a point. you currently have: " + up.TotalPoints + " points.";

            }
            else
            {
            successMessage = "You got " + correct + " Correct out of 4. Well done!";

            }

            return Json(successMessage);
        }




        public static IDictionary<int, string> LoadDictionary()
        {
            IDictionary<int, string> callNumbers = new Dictionary<int, string>();


            callNumbers.Add(0, "Computer Science, information and general works");
            callNumbers.Add(1, "Philosophy and Psycology");
            callNumbers.Add(2, "Religion");
            callNumbers.Add(3, "Social Sciences");
            callNumbers.Add(4, "Language");
            callNumbers.Add(5, "Pure Science");
            callNumbers.Add(6, "Technology");
            callNumbers.Add(7, "Arts and Recreation");
            callNumbers.Add(8, "Literature");
            callNumbers.Add(9, "History and Geography");


            return callNumbers;
        }
    }
}