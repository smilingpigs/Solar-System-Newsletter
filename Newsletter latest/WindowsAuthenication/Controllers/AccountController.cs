using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EY.SAF.Authentication;
using WindowsAuthenication.Models;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Configuration;
using WindowsAuthenication.Components;
using System.Data.SqlClient;
using WindowsAuthenication;
using System.Data.SQLite;
using System.Data;


namespace WindowsAuthenication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        public AccountController()
        //: this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //
        // POST: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Logger.Write("Enters Login method.", "Login");
            string loginType = ConfigurationManager.AppSettings["LoginType"].ToString();
            Logger.Write(loginType, "Login");
            ApplicationVariables.isAuthenticationTried = false;

            if (loginType == ApplicationConstrants.Manual)
            {
                Logger.Write("Manual Login section", "Login Manual");
                //testing 
                ViewBag.ReturnUrl = returnUrl;

                return View();
            }
            else if (loginType == ApplicationConstrants.Auto)
            {
                //Real implementation.
                Logger.Write("Auto Login Section", "Login Auto");
                Logger.Write("IsAuthenticationTried:" + System.Security.Principal.WindowsIdentity.GetCurrent().Name, "Login");
                if (!ApplicationVariables.isAuthenticationTried)
                {
                    string provider = "Windows";
                    return new ChallengeResult(provider, Url.Action("WindowsAuthLoginCallback", "Account", new { ReturnUrl = returnUrl }));

                    //string empNetworkId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    ////var abc = Request.ServerVariables["AUTH_USER"];
                    //Logger.Write("GetCurrent().Name:" + empNetworkId, "Login Auto");
                    ////ApplicationMethods.LogPath("GetCurrent().Name:" + empNetworkId);
                    //return LoginFunctionality(empNetworkId);
                }
                else
                {
                    Logger.Write("Auto Login Failed", "Login Auto");
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(ApplicationConstrants.SessionExpired);
                }
            }
            else
            {
                Logger.Write("Login Failed", "Login");
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(ApplicationConstrants.SessionExpired);
            }
        }




        public async Task<ActionResult> WindowsAuthLoginCallback(string returnUr)
        {
            SQLiteDataAdapter ad;
            
            DataTable dt = new DataTable();
            SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SqlLitDbConnection"].ToString());
            try
            {
                string userName = User.Identity.Name.Replace("MEA\\", "");
                string usernames= userName.Replace("."," ");
                Session["name"] = usernames;
                //string userName = "D.Kiruthika";
                //Session["userName"] = userName;
                //var userName = System.Environment.UserName;
                //var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                Dictionary<string, string> ADRetVal = ValidateUser.GetUserPropertiesFromAd(userName);
                string title = ADRetVal["title"];
                string location = ADRetVal["place"];
                

                //var abc = System.Web.HttpContext.Current.User;//;

                SQLiteCommand cmd;
                cmd = sqlite.CreateCommand();
                cmd.CommandText = "select * from UsersTable order by userid desc limit 1";
                sqlite.Open();
                //cmd.ExecuteReader();
                ad = new SQLiteDataAdapter(cmd);
                //sqlite.Close();
                ad.Fill(dt);
                sqlite.Close();
                int id = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    id = int.Parse(dt.Rows[i][0].ToString());
                }
                id++;
                sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SqlLitDbConnection"].ToString());
                //Initiate connection to the db
                var cmdText = "insert into UsersTable values (" + id + ",'" + userName + "','" + title + "','" + location + "')";
                cmd = sqlite.CreateCommand();
                sqlite.Open();
                cmd.CommandText = cmdText;  //set the passed query
                cmd.ExecuteNonQuery();
                //ad = new SQLiteDataAdapter(cmd);
                //ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            sqlite.Close(); 

            //Logger.Write("Windows Authentication method starts", "Login");

            //ApplicationVariables.isAuthenticationTried = true;

            //var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            //if (loginInfo == null)
            //{
            //    Logger.Write("Windows Authentication Failed!", "Login");
            //    return View("AuthorizeFailed");
            //}


            //string empNetworkId = string.Empty;
            //if (!string.IsNullOrEmpty(loginInfo.DefaultUserName))
            //{

            //    empNetworkId = loginInfo.DefaultUserName;
            //    // var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //    Logger.Write("empNetworkId: " + empNetworkId, "Login");
            //    // i have added the code
            //    // insert data to asp.net user table
            //   string userName = loginInfo.DefaultUserName.Replace("MEA\\", "");
            //    //string userName = "D.Kiruthika";
            //    Session["userName"] = userName;

            //    var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //    Dictionary<string, string> ADRetVal = ValidateUser.GetUserPropertiesFromAd(userName);
            //    if (ADRetVal.Count > 0)
            //    {
            //        ApplicationUser user = null;
            //        string email = ADRetVal["mail"].ToString();
            //        //user = UserManager.FindByName(userName);
            //        user = manager.FindByName(userName);
                   
            //        if (user != null)
            //        {
            //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //            SignInManager.SignIn(user, false, false);

            //            Logger.Write("User " + email + " signed in", "Login");
            //            Session["selEngName"] = "";
            //            SignInManager.SignIn(user, false, false);
            //            Session.Add(Constant.SignInUser, email);
            //            Session.Add(Constant.LogIn, email);
            //            Logger.Write("User " + userName + " signed in", "Login");
            //        }
            //        else
            //        {
            //            Logger.Write("New user Registration", "Login");
                        
            //            //{
            //            //Registration implementing here.
            //            user = new ApplicationUser { UserName = userName, Email = email };
            //            var result = UserManager.Create(user);
            //            UpdateUserDetails(ADRetVal, user);
            //            if (result.Succeeded)
            //            {
            //                //Logger.Write(AntiXssEncoder.HtmlEncode("New user " + user.Email + " Created", false), "Login");

            //                //Assign Role to user Here 
            //                if (ADRetVal["title"] == "Associate" || ADRetVal["title"] == "Senior Associate" || ADRetVal["title"] == "Senior")
            //                {
            //                    this.UserManager.AddToRole(user.Id, "User");
            //                }
            //                else
            //                {
            //                    this.UserManager.AddToRole(user.Id, "Admin");
            //                }
                           

            //                //Add Claim for each user to map projects.
            //                //UserManager.AddClaim(user.Id, new Claim(Constrants.UserClaimForProjects, Guid.NewGuid().ToString()));

            //                SignInManager.SignIn(user, false, false);

            //                AuthManager.SignOut();

            //                // SignIn without password.
            //                SignInManager.SignIn(user, false, false);
            //                //Session["LoggedIn"] = email;

            //                // SignIn without password.
            //                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            //                Session.Add(Constant.SignInUser, email);
            //                //Logger.Write(AntiXssEncoder.HtmlEncode("New user " + user.Email + " Signed in", false), "Login");
            //            }

            //        }
            //        //else
            //        //{
            //        //    string empTitle;
            //        //    var user1 = new ApplicationUser() { UserName = userName, Email = ADRetVal["mail"] };
            //        //    var result = UserManager.Create(user1);

            //        //    UpdateUserDetails(ADRetVal, user1);


            //        //    if (result.Succeeded)
            //        //    {

            //        //        //if (ADRetVal["title"] == "Associate" || ADRetVal["title"] == "Senior Associate" || ADRetVal["title"] == "Senior")
            //        //        //{
            //        //        //    empTitle = "AsstManager";
            //        //        //}
            //        //        //else
            //        //        //{
            //        //            empTitle = "Manager";
            //        //        //}

            //        //        this.UserManager.AddToRole(user.Id, empTitle);

            //        //        ApplicationVariables.loggedInUserEmailId = userName;


            //        //        //IdentityResult result1 = manager1.AddLogin(user1.Id, loginInfo1.Login);


            //        //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //        //        SignInManager.SignIn(user, false, false);
            //        //        Session.Add("LoggedIn", userName);
            //        //        Logger.Write("User Logged In:" + userName, "Authentication");



            //        //        //   return;




            //        //    }

            //        //}


            //    }
            //    return RedirectToAction(ApplicationConstrants.Index, ApplicationConstrants.Home);
            //}
            return RedirectToAction(ApplicationConstrants.Index, ApplicationConstrants.Home);
            //return View("Login");
        }


        

        public ActionResult SessionExpired()
        {
            return View();
        }
        private void UpdateUserDetails(Dictionary<string, string> ADRetVal, ApplicationUser user1)
        {

            //NewsLetterDbEntities dbNewsLetter = new NewsLetterDbEntities();
            //AspNetUser
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //con.Open();
            //cmd.CommandText = "Update AspNetUsers set Title=@Title,adspath=@adspath,department=@department,location=@location,distinguishedname=@distinguishedname,company=@company,state=@state,country=@country,address=@address,userprincipalname= @userprincipalname where Id=@Id";
            //cmd.Parameters.AddWithValue("@id", user1.Id);
            //if (ADRetVal["title"] == "Associate" || ADRetVal["title"] == "Senior Associate" || ADRetVal["title"] == "Senior")
            //{
            //    cmd.Parameters.AddWithValue("@Title", "AsstManager");
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@Title", "Manager");
            //}

            //cmd.Parameters.AddWithValue("@adspath", ADRetVal["adspath"]);
            //cmd.Parameters.AddWithValue("@department", ADRetVal["department"]);
            //cmd.Parameters.AddWithValue("@location", ADRetVal["place"]);
            //cmd.Parameters.AddWithValue("@distinguishedname", ADRetVal["distinguishedname"]);
            //cmd.Parameters.AddWithValue("@company", ADRetVal["company"]);
            //cmd.Parameters.AddWithValue("@state", ADRetVal["st"]);
            //cmd.Parameters.AddWithValue("@country", ADRetVal["co"]);
            //cmd.Parameters.AddWithValue("@address", ADRetVal["streetaddress"]);
            //cmd.Parameters.AddWithValue("@userprincipalname", ADRetVal["userprincipalname"]);
            //cmd.ExecuteNonQuery();
            //con.Close();

        }
        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            Logger.Write("Manual Login Method Starts...");
            if (string.IsNullOrEmpty(model.UserName))
            {
                return View(model);
            }

            return LoginFunctionality("MEA\\" + model.UserName);
        }

        public ActionResult LoginFunctionality(string empNetworkId)
        {
            Logger.Write("LoginFunctionality method starts", "Login");
            Logger.Write("empNetworkId: " + empNetworkId, "Login");
            ApplicationVariables.isAuthenticationTried = true;
            string userName = empNetworkId.Replace("MEA\\", "");
            //   if (db.ADAuthentications.Any(x => x.EmpNetworkId == empNetworkId))
            //    {
            Logger.Write("Employee network id exists", "Login");
            //      ADAuthentication adObj = db.ADAuthentications.Where(x => x.EmpNetworkId.Trim() == empNetworkId).FirstOrDefault();
            //Get user by UserName.
            //var user = UserManager.FindByName(adObj.EmpEmail.Replace("@in.ey.com", string.Empty));
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindByName(userName);

            if (user != null)
            {
                // SignIn without password.
                Logger.Write("User " + userName + " exists", "Login");
                ApplicationVariables.loggedInUserEmailId = userName;
                SignInManager.SignIn(user, false, false);
                Session.Add("LoggedIn", userName);
                Logger.Write("User " + userName + " signed in", "Login");
            }
            else
            {    // 'Admin' role user addition(registration) is implementing here
                //If user  is in admin role (find role by AD authentication)
                Logger.Write("New user Registration", "Login");

                //Registration implementing here.
                user = new ApplicationUser { UserName = userName };
                var result = UserManager.Create(user);

                if (result.Succeeded)
                {
                    Logger.Write("New user " + userName + " Created", "Login");

                    //Assign Role to user Here 
                    this.UserManager.AddToRole(user.Id, ApplicationConstrants.Manager);

                    //Add Claim for each user to map projects.
                    UserManager.AddClaim(user.Id, new Claim(ApplicationConstrants.UserClaimForProjects, Guid.NewGuid().ToString()));

                    ApplicationVariables.loggedInUserEmailId = userName;

                    // SignIn without password.
                    SignInManager.SignIn(user, false, false);
                    Session.Add("LoggedIn", userName);
                    Logger.Write("New user " + userName + " Signed in", "Login");
                }

            }
            return RedirectToAction(ApplicationConstrants.Index, ApplicationConstrants.Home);
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }


        //


        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}