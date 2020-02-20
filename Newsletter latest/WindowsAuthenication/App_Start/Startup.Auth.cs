using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using MohammadYounes.Owin.Security.MixedAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.DirectoryServices.AccountManagement;
using WindowsAuthenication.Models;
using WindowsAuthenication;
using Microsoft.AspNet.Identity.Owin;
using System;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
namespace WindowsAuthenication
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
       
            // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
            public void ConfigureAuth(IAppBuilder app)
            {// Configure the db context, user manager and signin manager to use a single instance per request
                app.CreatePerOwinContext(ApplicationDbContext.Create);
                app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
                app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

                // Enable the application to use a cookie to store information for the signed in user
                // and to use a cookie to temporarily store information about a user logging in with a third party login provider
                // Configure the sign in cookie
                CookieAuthenticationOptions cookieOptions = new CookieAuthenticationOptions
                {
                    CookieName = "LoginCookie",
                    //ExpireTimeSpan=TimeSpan.FromSeconds(10),
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    //SlidingExpiration=false,
                    LoginPath = new PathString("/Account/Login"),
                    Provider = new CookieAuthenticationProvider
                    {
                        // Enables the application to validate the security stamp when the user logs in.
                        // This is a security feature which is used when you change a password or add an external login to your account.  
                        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                            validateInterval: TimeSpan.FromMinutes(30),
                            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                    }
                };

                app.UseCookieAuthentication(cookieOptions);
                app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

                // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
                app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

                // Enables the application to remember the second login verification factor such as phone or email.
                // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
                // This is similar to the RememberMe option when you log in.
                app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);


                //mixed authentication
                app.UseMixedAuth(new MixedAuthOptions()
                {
                    Provider = new MixedAuthProvider()
                    {
                        OnImportClaims = identity =>
                        {
                            List<Claim> claims = new List<Claim>();

                            using (var principalContext = new PrincipalContext(ContextType.Domain)) //or ContextType.Machine
                            {
                                using (UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, identity.Name))
                                {
                                    if (userPrincipal != null)
                                    {
                                        claims.Add(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress ?? string.Empty));
                                        claims.Add(new Claim(ClaimTypes.Surname, userPrincipal.Surname ?? string.Empty));
                                        claims.Add(new Claim(ClaimTypes.GivenName, userPrincipal.GivenName ?? string.Empty));
                                    }
                                }
                            }
                            return claims;
                        }
                    }

                }, cookieOptions);


                // Uncomment the following lines to enable logging in with third party login providers
                //app.UseMicrosoftAccountAuthentication(
                //    clientId: "",
                //    clientSecret: "");

                //app.UseTwitterAuthentication(
                //   consumerKey: "",
                //   consumerSecret: "");

                //app.UseFacebookAuthentication(
                //   appId: "",
                //   appSecret: "");

                //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
                //{
                //    ClientId = "",
                //    ClientSecret = ""
                //});

                //// Enable the application to use a cookie to store information for the signed in user
                //app.UseCookieAuthentication(new CookieAuthenticationOptions
                //{
                //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //    LoginPath = new PathString("/Account/Login")
                //});
                //CookieAuthenticationOptions cookieOptions = new CookieAuthenticationOptions
                //{
                //    CookieName = "LoginCookie",
                //    //ExpireTimeSpan=TimeSpan.FromSeconds(10),
                //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //    //SlidingExpiration=false,
                //    LoginPath = new PathString("/Account/Login"),
                //    Provider = new CookieAuthenticationProvider
                //    {
                //        // Enables the application to validate the security stamp when the user logs in.
                //        // This is a security feature which is used when you change a password or add an external login to your account.  
                //        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                //            validateInterval: TimeSpan.FromMinutes(30),
                //            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                //    }
                //};

                //app.UseCookieAuthentication(cookieOptions);    

                //// Use a cookie to temporarily store information about a user logging in with a third party login provider
                //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

                ////mixed authentication
                //app.UseMixedAuth(new MixedAuthOptions()
                //{
                //    Provider = new MixedAuthProvider()
                //    {
                //        OnImportClaims = identity =>
                //        {
                //            List<Claim> claims = new List<Claim>();

                //            using (var principalContext = new PrincipalContext(ContextType.Domain)) //or ContextType.Machine
                //            {
                //                using (UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, identity.Name))
                //                {
                //                    if (userPrincipal != null)
                //                    {
                //                        claims.Add(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress ?? string.Empty));
                //                        claims.Add(new Claim(ClaimTypes.Surname, userPrincipal.Surname ?? string.Empty));
                //                        claims.Add(new Claim(ClaimTypes.GivenName, userPrincipal.GivenName ?? string.Empty));
                //                    }
                //                }
                //            }
                //            return claims;
                //        }
                //    }

                //}, cookieOptions);
                //// Uncomment the following lines to enable logging in with third party login providers
                ////app.UseMicrosoftAccountAuthentication(
                ////    clientId: "",
                ////    clientSecret: "");

                ////app.UseTwitterAuthentication(
                ////   consumerKey: "",
                ////   consumerSecret: "");

                ////app.UseFacebookAuthentication(
                ////   appId: "",
                ////   appSecret: "");

                ////app.UseGoogleAuthentication();
            }
        }
    }
