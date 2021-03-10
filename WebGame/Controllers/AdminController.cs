using WebGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using OfficeOpenXml;
using System.Web.UI;
using System.Text;
using System.Globalization;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;


namespace WebGame.Controllers
{
    public class AdminController : BaseController
    {
        List_Game_DB list_Game_DB = new List_Game_DB();
        // GET: Admin
        [Route("Manage-Games")]
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        //--List Game--\\
        public ActionResult ListGame([DataSourceRequest] DataSourceRequest request)
        {
            var dataList = Json(list_Game_DB.ListAllGame().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            dataList.MaxJsonLength = int.MaxValue;
            return dataList;
        }
        //--GetbyID Game--\\
        public JsonResult GetbyIDGame(int ID)
        {
            var game = list_Game_DB.ListAllGame().Find(x => x.ID.Equals(ID));
            return Json(game, JsonRequestBehavior.AllowGet);
        }
        //--Add New Game--\\
        public ActionResult AddNewGame()
        {
            var game = new List_Game_Model();
            game.Name = Request["Name"];
            game.LinkIOS = Request["LinkIOS"];
            game.LinkAndroid = Request["LinkAndroid"];
            game.Caption = Request["Caption"];
            game.Featured_Games = Convert.ToBoolean(Request["Featured"]);
            byte[] image = null;
            var file = Request.Files[0];
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                image = binaryReader.ReadBytes(file.ContentLength);
            }
            game.Image = image;
            return Json(list_Game_DB.AddNew(game), JsonRequestBehavior.AllowGet);
        }
        //--Update Game--\\
        public ActionResult UpdateGame()
        {
            var game = new List_Game_Model();
            game.ID = Convert.ToInt32(Request["ID"]);
            game.Name = Request["Name"];
            game.LinkIOS = Request["LinkIOS"];
            game.LinkAndroid = Request["LinkAndroid"];
            game.Caption = Request["Caption"];

            byte[] image = null;
            var file = Request.Files[0];
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                image = binaryReader.ReadBytes(file.ContentLength);
            }
            game.Image = image;
            return Json(list_Game_DB.Update(game), JsonRequestBehavior.AllowGet);
        }
        //--Delete Game--\\
        public ActionResult DeleteGame(int ID)
        {
            var game = list_Game_DB.Delete(ID);
            return Json(game, JsonRequestBehavior.AllowGet);
        }
        //--View Image--\\
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
        [HttpGet]
        public ActionResult ViewImageStory1(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var story = entities.OurStoryAboutPages.FirstOrDefault(select => select.ID == id);
                if (story != null)
                {
                    if (story.PictureMaxWidth != null)
                    {
                        byte[] image = story.PictureMaxWidth;
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
        [HttpGet]
        public ActionResult ViewImageStory2(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var story = entities.OurStoryAboutPages.FirstOrDefault(select => select.ID == id);
                if (story != null)
                {
                    if (story.PictureWidth640 != null)
                    {
                        byte[] image = story.PictureWidth640;
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
        [HttpGet]
        public ActionResult ViewImageGame(int id)
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
        //--Read File CV--\\
        [HttpGet]
        public ActionResult ReadFile(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var applyJob = entities.Apply_Job.FirstOrDefault(select => select.ID == id);
                if (applyJob != null)
                {
                    if (applyJob.CV != null)
                    {
                        byte[] fileCV = applyJob.CV;
                        return File(fileCV, "application/pdf", string.Format("{0}.pdf", id));
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
        //-----------------\\
        //--Manage Account--\\
        //-------------------\\
        //--Account List--\\
        AccountDB accountDB = new AccountDB();
        [Route("Manage-Account")]
        public ActionResult ListAccount()
        {
            return View();
        }
        //--Read Account List--\\
        public ActionResult ReadAccountList([DataSourceRequest] DataSourceRequest request)
        {
            var dataListAccount = Json(accountDB.ListAllAccount().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            dataListAccount.MaxJsonLength = int.MaxValue;
            return dataListAccount;
        }
        //--Create Account--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult CreateAccount([DataSourceRequest] DataSourceRequest request, Account_Model account_Model)
        {
            if (account_Model != null && ModelState.IsValid)
            {
                accountDB.CreateAccount(account_Model);
            }
            return Json(new[] { account_Model }.ToDataSourceResult(request));
        }
        //--Update Account--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult UpdateAccount([DataSourceRequest] DataSourceRequest request, Account_Model account_Model)
        {
            if (account_Model != null && ModelState.IsValid)
            {
                accountDB.UpdateAccount(account_Model);
            }

            return Json(new[] { account_Model }.ToDataSourceResult(request));
        }
        //--Delete Account--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult DeleteAccount([DataSourceRequest] DataSourceRequest request, Account_Model account_Model)
        {
            if (account_Model != null)
            {
                accountDB.DeleteAccount(account_Model);
            }

            return Json(new[] { account_Model }.ToDataSourceResult(request));
        }
        //------------\\
        //Submit Game--\\
        GameSubmit_DB gameSubmit_DB = new GameSubmit_DB();
        [Route("Manage-Game-Submit")]
        public ActionResult SubmitGame()
        {
            return View();
        }
        //--List Submit Game--\\
        public ActionResult ListSubmitGame([DataSourceRequest] DataSourceRequest request)
        {
            var dataList = Json(gameSubmit_DB.ListAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            dataList.MaxJsonLength = int.MaxValue;
            return dataList;
        }
        //--Delete Game Submit--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult DeleteSubmitGame([DataSourceRequest] DataSourceRequest request, GameSubmit_Model gameSubmit_Model)
        {
            if (gameSubmit_Model != null)
            {
                gameSubmit_DB.Delete(gameSubmit_Model);
            }
            return Json(new[] { gameSubmit_Model }.ToDataSourceResult(request));
        }
        //--Get Game Submit by ID--\\
        public JsonResult GetbyIDGameSubmit(int ID)
        {
            var gameSubmit = gameSubmit_DB.ListAll().Find(x => x.ID.Equals(ID));
            return Json(gameSubmit, JsonRequestBehavior.AllowGet);
        }
        //--List Jobs--\\
        List_Jobs_DB list_Jobs_DB = new List_Jobs_DB();
        [Route("Manage-Jobs")]
        public ActionResult ListJobs()
        {
            return View();
        }
        //--Read List Jobs--\\
        public ActionResult ReadListJobs([DataSourceRequest] DataSourceRequest request)
        {
            var listJobs = Json(list_Jobs_DB.ListAllJobs().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            listJobs.MaxJsonLength = int.MaxValue;
            return listJobs;
        }
        //--Add Job--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult AddJob([DataSourceRequest] DataSourceRequest request, Job_Model list_Jobs_Model)
        {
            if (list_Jobs_Model != null && ModelState.IsValid)
            {
                list_Jobs_DB.AddJob(list_Jobs_Model);
            }
            return Json(new[] { list_Jobs_Model }.ToDataSourceResult(request));
        }
        //--Get Job by ID--\\
        public JsonResult GetbyIDJob(int ID)
        {
            var job = list_Jobs_DB.ListAllJobs().Find(x => x.ID.Equals(ID));
            return Json(job, JsonRequestBehavior.AllowGet);
        }
        //--Update Job--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult UpdateJob([DataSourceRequest] DataSourceRequest request, Job_Model list_Jobs_Model)
        {
            if (list_Jobs_Model != null && ModelState.IsValid)
            {
                list_Jobs_DB.UpdateJob(list_Jobs_Model);
            }
            return Json(new[] { list_Jobs_Model }.ToDataSourceResult(request));
        }
        //--Delete Job--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult DeleteJob([DataSourceRequest] DataSourceRequest request, Job_Model list_Jobs_Model)
        {
            if (list_Jobs_Model != null)
            {
                list_Jobs_DB.Delete(list_Jobs_Model);
            }
            return Json(new[] { list_Jobs_Model }.ToDataSourceResult(request));
        }
        //------------\\
        //--Apply Jobs--\\
        //--List Apply Jobs--\\
        [Route("Manage-Apply-Jobs")]
        public ActionResult ListApplyJobs()
        {
            return View();
        }
        //--Read List Apply Jobs--\\
        Apply_Job_DB apply_Job_DB = new Apply_Job_DB();
        public ActionResult ReadListApplyJobs([DataSourceRequest] DataSourceRequest request)
        {
            var list = Json(apply_Job_DB.ListAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            list.MaxJsonLength = int.MaxValue;
            return list;
        }
        //--Get by ID Apply Jobs--\\
        public JsonResult GetbyIDApplyJobs(int ID)
        {
            var applyJob = apply_Job_DB.ListAll().Find(x => x.ID.Equals(ID));
            return Json(applyJob, JsonRequestBehavior.AllowGet);
        }
        //--Delete Apply Jobs--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult DeleteApplyJobs([DataSourceRequest] DataSourceRequest request, Apply_Job_Model apply_Job_Model)
        {
            if (apply_Job_Model != null)
            {
                apply_Job_DB.Delete(apply_Job_Model);
            }
            return Json(new[] { apply_Job_Model }.ToDataSourceResult(request));
        }
        //--List Contact--\\
        [Route("Manage-Contact")]
        public ActionResult ListContact()
        {
            return View();
        }
        //--Read List Contact--\\
        Contact_DB contact_DB = new Contact_DB();
        public ActionResult ReadListContact([DataSourceRequest] DataSourceRequest request)
        {
            var list = Json(contact_DB.ListAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            list.MaxJsonLength = int.MaxValue;
            return list;
        }
        //--Get contact by ID--\\
        public JsonResult GetContactbyID(int ID)
        {
            var contact = contact_DB.ListAll().Find(x => x.ID.Equals(ID));
            return Json(contact, JsonRequestBehavior.AllowGet);
        }
        //--Delete Contact--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult DeleteContact([DataSourceRequest] DataSourceRequest request, Contact_Model contact_Model)
        {
            if (contact_Model != null)
            {
                contact_DB.Delete(contact_Model);
            }
            return Json(new[] { contact_Model }.ToDataSourceResult(request));
        }
        //--Manage Our Story--\\
        [Route("Manage-Our-Story")]
        public ActionResult OurStory()
        {
            return View();
        }
        //--Read List Our Story--\\
        OurStory_DB ourStory_DB = new OurStory_DB();
        public ActionResult ReadListStory([DataSourceRequest] DataSourceRequest request)
        {
            var listStory = Json(ourStory_DB.ListAllStory().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            listStory.MaxJsonLength = int.MaxValue;
            return listStory;
        }
        //--Add Our Story--\\
        public ActionResult AddStory()
        {
            var story = new OurStory_Model();
            story.OurStoryName = Request["Name"];
            story.Title = Request["Title"];
            byte[] picture1 = null;
            var file1 = Request.Files[0];
            using (var binaryReader1 = new BinaryReader(file1.InputStream))
            {
                picture1 = binaryReader1.ReadBytes(file1.ContentLength);
            }
            story.PictureMaxWidth = picture1;

            byte[] picture2 = null;
            var file2 = Request.Files[1];
            using (var binaryReader2 = new BinaryReader(file2.InputStream))
            {
                picture2 = binaryReader2.ReadBytes(file2.ContentLength);
            }
            story.PictureWidth640 = picture2;
            return Json(ourStory_DB.AddNew(story), JsonRequestBehavior.AllowGet);
        }
        //--Get Our Story by ID--\\
        public JsonResult GetStorybyID(int ID)
        {
            var story = ourStory_DB.ListAllStory().Find(x => x.ID.Equals(ID));
            return Json(story, JsonRequestBehavior.AllowGet);
        }
        //--Update Our Story--\\
        public ActionResult UpdateStory()
        {
            var story = new OurStory_Model();
            story.ID = Convert.ToInt32(Request["ID"]);
            story.OurStoryName = Request["Name"];
            story.Title = Request["Title"];
            
            if(Request["Picture1"] != "" && Request["Picture2"] != "")
            {
                byte[] picture1 = null;
                var file1 = Request.Files[0];
                using (var binaryReader1 = new BinaryReader(file1.InputStream))
                {
                    picture1 = binaryReader1.ReadBytes(file1.ContentLength);
                }
                story.PictureMaxWidth = picture1;

                byte[] picture2 = null;
                var file2 = Request.Files[1];
                using (var binaryReader2 = new BinaryReader(file2.InputStream))
                {
                    picture2 = binaryReader2.ReadBytes(file2.ContentLength);
                }
                story.PictureWidth640 = picture2;
            }
            else if(Request["Picture1"] != "" && Request["Picture2"] == "")
            {
                byte[] picture1 = null;
                var file1 = Request.Files[0];
                using (var binaryReader1 = new BinaryReader(file1.InputStream))
                {
                    picture1 = binaryReader1.ReadBytes(file1.ContentLength);
                }
                story.PictureMaxWidth = picture1;
            }
            else if(Request["Picture1"] == "" && Request["Picture2"] != "")
            {
                byte[] picture2 = null;
                var file2 = Request.Files[0];
                using (var binaryReader2 = new BinaryReader(file2.InputStream))
                {
                    picture2 = binaryReader2.ReadBytes(file2.ContentLength);
                }
                story.PictureWidth640 = picture2;
            }

            return Json(ourStory_DB.Update(story), JsonRequestBehavior.AllowGet);
        }
        //--Delete Our Story--\\
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult DeleteStory([DataSourceRequest] DataSourceRequest request, OurStory_Model ourStory_Model)
        {
            if (ourStory_Model != null)
            {
                ourStory_DB.Delete(ourStory_Model);
            }
            return Json(new[] { ourStory_Model }.ToDataSourceResult(request));
        }
    }
}