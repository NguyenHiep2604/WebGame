using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebGame.Models;

namespace WebGame.Controllers
{
    public class AccountController : Controller
    {
        //--Login--\\
        public ActionResult Login(Account_Model account)
        {
            var userID = CheckLoginCookie();
            if (userID != null)
            {
                using (WebGameEntities context = new WebGameEntities())
                {
                    int user = Convert.ToInt32(userID);
                    var userAccount = context.Accounts.FirstOrDefault(s => s.ID == user);
                    if (userAccount != null)
                    {
                        Session["ID"] = userAccount.ID.ToString();
                        Session["Email"] = userAccount.Email.ToString();
                        Session["Name"] = userAccount.UserName.ToString();
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }
        //--Check Login--\\
        public ActionResult CheckLogin(Account_Model account, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                using (WebGameEntities context = new WebGameEntities())
                {
                    var userAccount = context.Accounts.Where(user => user.Email.Equals(account.Email) && user.PassWord.Equals(account.PassWord)).FirstOrDefault();
                    if (userAccount != null)
                    {
                        Session["ID"] = userAccount.ID.ToString();
                        Session["Email"] = userAccount.Email.ToString();
                        Session["Name"] = userAccount.UserName.ToString();
                        var cookieText = System.Text.Encoding.UTF8.GetBytes(userAccount.ID.ToString());
                        var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "ProtectCookie"));

                        //--- Create cookie object and pass name of the cookie and value to be stored.
                        HttpCookie cookieObject = new HttpCookie("auth", encryptedValue);

                        //---- Set expiry time of cookie.
                        cookieObject.Expires.AddDays(5);
                        Response.Cookies.Add(cookieObject);
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.Message = "Sai thông tin đăng nhập!";
                    }
                }
            }
            return View("Login");
        }
        //--CheckLogin Cookie--\\
        private string CheckLoginCookie()
        {
            if (Request.Cookies["auth"] != null)
            {
                var bytes = Convert.FromBase64String(Request.Cookies["auth"].Value);
                var output = MachineKey.Unprotect(bytes, "ProtectCookie");

                string userID = System.Text.Encoding.UTF8.GetString(output);
                return userID;
            }
            else
            {
                return null;
            }
        }
        //--LogOut--\\
        public ActionResult LogOut()
        {
            string[] myCookie = Request.Cookies.AllKeys;
            foreach (string cookie in myCookie)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            FormsAuthentication.SignOut();
            Session.Clear();
            return View("Login");
        }
        //--Change Password--\\
        public ActionResult ChangePassWord()
        {
            return View();
        }
        //--Valid Recaptcha--\\
        public bool ValidRecaptcha()
        {
            string EncodedResponse = Request["g-Recaptcha-Response"];

            var client = new System.Net.WebClient();

            string PrivateKey = "6LdW0eMZAAAAADlQY_w3d9zO03fPf_QyYSv6OmWC";

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

            var captchaResponse = JsonConvert.DeserializeObject<reCaptchaModel>(GoogleReply);

            bool IsCaptchaValid = (captchaResponse.Success == "true" ? true : false);
            if (IsCaptchaValid == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //--Send Email--\\
        AccountDB accountDB = new AccountDB();
        public ActionResult SendEmail(Account account)
        {
            var recaptchaValid = ValidRecaptcha();
            if (recaptchaValid == true)
            {
                if (ModelState.IsValid)
                {
                    using (WebGameEntities context = new WebGameEntities())
                    {
                        var userAccount = context.Accounts.Where(user => user.Email.Equals(account.Email)).FirstOrDefault();
                        if (userAccount != null)
                        {
                            var accountUser = accountDB.ListAllAccount().Find(x => x.Email.Equals(account.Email));
                            var name = accountUser.UserName;
                            var userID = accountUser.ID;

                            var token = "/Account/ForgotPassword/" + userID;
                            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, token);

                            var Email = userAccount.Email;
                            //var Emai = ConfigurationManager.AppSettings["Email"];
                            var Email_Password = ConfigurationManager.AppSettings["Email_Password"];
                            var Email_Smtp = ConfigurationManager.AppSettings["Email_Smtp"];
                            var Port = ConfigurationManager.AppSettings["Port"];

                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient(Email_Smtp);

                            mail.From = new MailAddress(Email);
                            mail.To.Add(account.Email);
                            mail.Subject = "Forgot password !";

                            mail.IsBodyHtml = true;

                            var path = Server.MapPath("/Template/Email_ForgotPassword.html");
                            StreamReader html = new StreamReader(path);
                            string htmlBody = html.ReadToEnd();
                            htmlBody = htmlBody.Replace("$name$", name).Replace("$link$", link);
                            html.Close();

                            mail.Body = htmlBody;

                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(Email, Email_Password);
                            SmtpServer.EnableSsl = true;

                            SmtpServer.Send(mail);
                            return RedirectToAction("SendEmailSuccess", "Account");
                        }
                        else
                        {
                            ViewBag.Message = "Người dùng không tồn tại.";
                        }
                    }
                }
            }
            return View("ChangePassword");
        }
        //--Send Email Success--\\
        public ActionResult SendEmailSuccess()
        {
            return View();
        }
        //--Forgot Password--\\
        public ActionResult ForgotPassword()
        {
            return View();
        }
        //--Get by ID Account--\\
        public ActionResult GetbyIDAccount(int ID)
        {
            var account = accountDB.ListAllAccount().Find(x => x.ID.Equals(ID));
            return Json(account, JsonRequestBehavior.AllowGet);
        }
        public class reCaptchaModel
        {
            [JsonProperty("success")]
            public string Success
            {
                get { return m_Success; }
                set { m_Success = value; }
            }

            private string m_Success;
            [JsonProperty("error-codes")]
            public List<string> ErrorCodes
            {
                get { return m_ErrorCodes; }
                set { m_ErrorCodes = value; }
            }

            private List<string> m_ErrorCodes;
        }
    }
}