using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class Apply_Job_DB
    {
        //--Submit Apply Job--\\
        public Apply_Job_Model SubmitApplyJob(Apply_Job_Model apply_Job_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                Apply_Job apply_Job = new Apply_Job();
                apply_Job.FullName = apply_Job_Model.FullName;
                apply_Job.Email = apply_Job_Model.Email;
                apply_Job.Phone = apply_Job_Model.Phone;
                apply_Job.CurrentCompany = apply_Job_Model.CurrentCompany;
                apply_Job.CV = apply_Job_Model.CV;
                apply_Job.LinkedInURL = apply_Job_Model.LinkedInURL;
                apply_Job.TwitterURL = apply_Job_Model.TwitterURL;
                apply_Job.GithubURL = apply_Job_Model.GithubURL;
                apply_Job.PortfolioURL = apply_Job_Model.PortfolioURL;
                apply_Job.OtherWebsite = apply_Job_Model.OtherWebsite;
                apply_Job.More = apply_Job_Model.More;
                entities.Apply_Job.Add(apply_Job);
                entities.SaveChanges();
                return apply_Job_Model;
            }
        }
        //--List Apply Jobs--\\
        public List<Apply_Job_Model> ListAll()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var list = (from data in entities.Apply_Job
                            select new Apply_Job_Model
                            {
                                ID = data.ID,
                                FullName = data.FullName,
                                Email = data.Email,
                                Phone = data.Phone,
                                CurrentCompany = data.CurrentCompany,
                                CV = data.CV,
                                LinkedInURL = data.LinkedInURL,
                                TwitterURL = data.TwitterURL,
                                GithubURL = data.GithubURL,
                                PortfolioURL = data.PortfolioURL,
                                OtherWebsite = data.OtherWebsite,
                                More = data.More
                            }).OrderByDescending(n => n.ID).ToList();
                return list;
            }
        }
        //--Delete Apply Jobs--\\
        public Apply_Job_Model Delete(Apply_Job_Model apply_Job)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                Apply_Job_Model apply_Job_Model = new Apply_Job_Model();
                var delete = entities.Apply_Job.FirstOrDefault(x => x.ID == apply_Job.ID);
                entities.Apply_Job.Remove(delete);
                entities.SaveChanges();
                return apply_Job_Model;
            }
        }
    }
}