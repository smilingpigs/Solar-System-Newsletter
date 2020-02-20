using EY.SAF.Authentication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindowsAuthenication.Models;

namespace WindowsAuthenication.Controllers
{
    public class HomeController : Controller
    {
        private SQLiteConnection sqlite;


        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    //ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    // ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        //public ActionResult EditorNote()
        //{
        //    return View();

        //}
        //public ActionResult Spotlight()
        //{
        //    return View();

        //}
        //public ActionResult NewsYouCanUse()
        //{
        //    return View();

        //}
        //public ActionResult EYBuzzPipeline()
        //{
        //    return View();

        //}
        //public ActionResult ARCreactor()
        //{
        //    return View();

        //}
        //public ActionResult InnovationWeekCampaign()
        //{
        //    return View();

        //}
        //public ActionResult OutcomeOfTheCampaign()
        //{
        //    return View();

        //}
        //public ActionResult Invention()
        //{
        //    return View();

        //}
        //public ActionResult YamJamQuotes()
        //{
        //    return View();

        //}

        public ActionResult GetUserLst()
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();
            SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SqlLitDbConnection"].ToString());
            try
            {
                SQLiteCommand cmd;
                cmd = sqlite.CreateCommand();
                cmd.CommandText = "select * from UsersTable order by userid";
                sqlite.Open();
                //cmd.ExecuteReader();
                ad = new SQLiteDataAdapter(cmd);
                //sqlite.Close();
                ad.Fill(dt);
                sqlite.Close();
                int id = 0;
                List<UserViewModel> lst = new List<UserViewModel>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserViewModel obj = new UserViewModel();
                    obj.UserName = dt.Rows[i][1].ToString().Replace("."," ");
                    obj.Designation = dt.Rows[i][2].ToString();
                    obj.Location = dt.Rows[i][3].ToString();
                    lst.Add(obj);
                }

                Session["Lst"] = lst;
                Session["TotalCount"] = lst.Count();
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            return View();
        }
    }
}