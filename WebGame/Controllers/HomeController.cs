using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGame.Models;

namespace WebGame.Controllers
{
    public class HomeController : Controller
    {
        const string DefaultLangCode = "en";
        //
        //ChangeCulture
        //
        public ActionResult ChangeCulture(string id)
        {
            Session["language"] = id;
            return Redirect(Request.UrlReferrer.ToString());
        }
        //--Index--\\
        public ActionResult Index()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listGameFeatured = from gameFeatured in entities.List_Game
                                       where gameFeatured.Featured_Games == true
                                       select gameFeatured;
                ViewBag.DataListGameFeatured = listGameFeatured.ToList();
            }
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--Game--\\
        public ActionResult Games()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listGameFeatured = from gameFeatured in entities.List_Game
                                       where gameFeatured.Featured_Games == true
                                       select gameFeatured;
                ViewBag.DataListGameFeatured = listGameFeatured.ToList();

                var listAllGame = from game in entities.List_Game
                                  select game;
                ViewBag.DataListAllGame = listAllGame.ToList();
            }
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--About--\\
        public ActionResult About()
        {
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--Jobs--\\
        public ActionResult Jobs()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listJob = from job in entities.List_Jobs
                              select job;
                ViewBag.ListJob = listJob.ToList();

                //var listGroup = listJob.GroupBy(a => a.Team).Select(grp => grp.ToList()).ToList();

                var listGroup = from p in entities.List_Jobs
                                group p by p.Team into g
                                select new GroupJob {
                                    GroupName = g.Key,
                                    Jobs = (from i in g.ToList()
                                            select new Job_Model { 
                                            ID = i.ID,
                                            Description = i.Description,
                                            Location = i.Location,
                                            Team = i.Team,
                                            Vacancies = i.Vacancies
                                            }).ToList()
                                };

                //var groupJob = listGroup.GroupBy(group => group.Group).Select(a => a.ToList());
                ViewBag.GroupJob = listGroup.ToList();

                var listJobsAnalytics = from jobAnalytics in entities.List_Jobs
                                        where jobAnalytics.Team == "Analytics"
                                        select jobAnalytics;
                ViewBag.ListJobsAnalytics = listJobsAnalytics.ToList();

                var listBusinessDev = from jobBusiness in entities.List_Jobs
                                      where jobBusiness.Team == "Business Development"
                                      select jobBusiness;
                ViewBag.ListJobsBusinessDev = listBusinessDev.ToList();
            }
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--Case--\\
        public ActionResult Case()
        {
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--FAQ--\\
        public ActionResult FAQ()
        {
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--Game Submissions--\\
        public ActionResult GameSubmissions()
        {
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--Contact--\\
        public ActionResult Contact()
        {
            using(WebGameEntities entities = new WebGameEntities())
            {
                var listGame = from game in entities.List_Game
                               select game;
                ViewBag.ListGame = listGame.ToList();
            }
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--Game Submit--\\
        GameSubmit_DB gameSubmit_DB = new GameSubmit_DB();
        public ActionResult SubmitGame()
        {
            bool recaptcha = ValidRecaptcha();
            if(recaptcha == true)
            {
                GameSubmit_Model gameSubmit_Model = new GameSubmit_Model();
                gameSubmit_Model.FullName = Request["FullName"];
                gameSubmit_Model.Email = Request["Email"];
                gameSubmit_Model.GameTitle = Request["Title"];
                gameSubmit_Model.VideoFootageLink = Request["FootageLink"];
                gameSubmit_Model.CompanyName = Request["CompanyName"];
                gameSubmit_Model.Country = Request["Country"];
                gameSubmit_Model.LinkAppStore = Request["LinkStore"];
                gameSubmit_Model.MoreAbout = Request["MoreAbout"];
                gameSubmit_Model.DateSubmit = DateTime.Now;

                var game = gameSubmit_DB.GameSubmit(gameSubmit_Model);
                return Json(game, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        //--Details Description Job--\\
        public ActionResult DetailsJob(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listJob = from job in entities.List_Jobs
                                     select job;
                ViewBag.Data = listJob.ToList();
                var Job = listJob.Where(b => b.ID == id).FirstOrDefault();
                ViewBag.ID = Job.ID;
                ViewBag.Team = Job.Team;
                ViewBag.Location = Job.Location;
                ViewBag.Vacancies = Job.Vacancies;
                ViewBag.Description = Job.Description;
            }
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //--Apply for job--\\
        public ActionResult ApplyJob(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listJob = from job in entities.List_Jobs
                              select job;
                ViewBag.Data = listJob.ToList();
                var Job = listJob.Where(b => b.ID == id).FirstOrDefault();
                ViewBag.Team = Job.Team;
                ViewBag.Location = Job.Location;
                ViewBag.Vacancies = Job.Vacancies;
                ViewBag.Description = Job.Description;
            }
            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return View();
        }
        //-----------\\
        //--Apply Job--\\
        //--Submit Apply Job--\\
        Apply_Job_DB apply_Job_DB = new Apply_Job_DB();
        public ActionResult SubmitApplyJob()
        {
            Apply_Job_Model apply_Job_Model = new Apply_Job_Model();
            apply_Job_Model.FullName = Request["FullName"];
            apply_Job_Model.Email = Request["Email"];
            apply_Job_Model.Phone = Request["Phone"];
            byte[] file = null;
            var fileSubmit = Request.Files[0];
            using (var binaryReader = new BinaryReader(fileSubmit.InputStream))
            {
                file = binaryReader.ReadBytes(fileSubmit.ContentLength);
            }
            apply_Job_Model.CV = file;
            apply_Job_Model.CurrentCompany = Request["CurrentCompany"];
            apply_Job_Model.LinkedInURL = Request["LinkedInURL"];
            apply_Job_Model.TwitterURL = Request["TwitterURL"];
            apply_Job_Model.GithubURL = Request["GithubURL"];
            apply_Job_Model.PortfolioURL = Request["PortfolioURL"];
            apply_Job_Model.OtherWebsite = Request["OtherWebsite"];
            apply_Job_Model.More = Request["More"];

            var submit = apply_Job_DB.SubmitApplyJob(apply_Job_Model);

            ViewBag.LangCode = Session["language"] ?? DefaultLangCode;
            return Json(submit, JsonRequestBehavior.AllowGet);
        }
        //--View Image
        [HttpGet]
        public ActionResult ViewImage(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var game = entities.List_Game.FirstOrDefault(select => select.ID == id);
                if (game != null)
                {
                    if (game.Image != null)
                    {
                        byte[] image = game.Image;
                        return File(image, "image/jpg", string.Format("{0}.jpg", id));
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        //--Valid Recaptcha--\\
        public bool ValidRecaptcha()
        {
            string EncodedResponse = Request["g-Recaptcha-Response"];

            var client = new System.Net.WebClient();

            string PrivateKey = "6LfQtmwaAAAAAAP7HR_hPw3M7tLLABHJrRhY2LuZ";

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

        Contact_DB contact_DB = new Contact_DB();
        //--Submit Contact--\\
        public ActionResult SubmitContact()
        {
            Contact_Model contact_Model = new Contact_Model();
            contact_Model.FullName = Request["FullName"];
            contact_Model.Email = Request["Email"];
            contact_Model.Subject = Request["Subject"];
            Game_Model game_Model = new Game_Model();
            game_Model.ID = Convert.ToInt32(Request["Optional"]);
            contact_Model.Message = Request["Message"];
            var submitContact = contact_DB.AddContact(contact_Model);
            return Json(submitContact, JsonRequestBehavior.AllowGet);
        }
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