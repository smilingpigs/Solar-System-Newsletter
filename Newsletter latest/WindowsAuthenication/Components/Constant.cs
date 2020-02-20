using WindowsAuthenication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace WindowsAuthenication.Components
{
    public static class Constant
    {
       
     
       
        public const string SubEngagementID = "SubEngID";
       
        public const string SignInUser = "SignedInUser";
        public const string LogIn = "LoggedIn";
       
    }

    public static class ApplicationConstrants
    {
        public const string Engagements = "Engagements";
        public const string Creator = "Creator";
        public const string Home = "Home";
        public const string Account = "Account";
        public const string Index = "Index";
        public const string UserClaimForProjects = "UserClaimForProjects";
        public const string Create = "Create";
        public const string Delete = "Delete";
        public const string Member = "Member";
        public const string ProjectDetails = "ProjectDetails";
        public const string Import = "Import";
        public const string Approved = "Approved";
        public const string Unapproved = "Unapproved";
        public const string Rejected = "Rejected";
        public const string Analyses = "Analyses";
        public const string Manager = "Manager";
        public const string CANNOTUSED = "CANNOTUSED";
        public const string NOTUSED = "NOTUSED";
        public const string SessionExpired = "SessionExpired";
        public const string USED = "USED";
        public const string Manual = "Manual";
        public const string Auto = "Auto";
    }
    public static class ApplicationVariables
    {
        public static string selectedProjectId = string.Empty;
        public static string selectedProjectName = string.Empty;
        public static bool isAuthenticationTried = false;
        public static string loggedInUserEmailId = string.Empty;
        public static RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

        public static void ClearApplicationVariables()
        {
            selectedProjectId = string.Empty;
            selectedProjectName = string.Empty;
            loggedInUserEmailId = string.Empty;
            isAuthenticationTried = false;
        }

    }

    public static class ApplicationMethods
    {

        public static void LogPath(string param1)
        {
            //string filePath = @"C:\Gopi\EYQS\LogPath.txt";
            string filePath = ConfigurationManager.AppSettings["LogPath"] + "LogPath.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(param1 + "  Date :" + DateTime.Now.ToString() + Environment.NewLine);

            }
        }

        public static void LogErrors(Exception ex)
        {
            //string filePath = @"C:\Gopi\EYQS\EYQSErrorLog.txt";
            string filePath = ConfigurationManager.AppSettings["LogPath"] + "DeferredtaxError.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Method:AnalysesUpload" + Environment.NewLine);
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace + "<br/>" + Environment.NewLine + "Data :" + ex.Data + "<br/>" + Environment.NewLine + "Inner Exception :" + ex.InnerException + "<br/>" + Environment.NewLine + "Source :" + ex.Source + "<br/>" + Environment.NewLine + "TargetSite :" + ex.TargetSite +
                   "<br/>" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "----------------------------------------------------------------------------ˆn7‰IÊÚ£¿fôı»4Vàª£÷ãGNæoªP)H»”jVÁ7i‘ê∫/≈πPÇ∫Êök+8p»◊M.