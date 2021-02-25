using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class List_Jobs_DB
    {
        //--List All Jobs--\\
        public List<Job_Model> ListAllJobs()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listJobs = (from job in entities.List_Jobs
                                select new Job_Model
                                {
                                    ID = job.ID,
                                    Team = job.Team,
                                    Location = job.Location,
                                    Description = job.Description,
                                    Vacancies = job.Vacancies
                                }).OrderByDescending(job => job.ID).ToList();
                return listJobs;
            }
        }
        //--Add Job--\\
        public Job_Model AddJob(Job_Model list_Jobs_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                List_Jobs list_Jobs = new List_Jobs();
                list_Jobs.Team = list_Jobs_Model.Team;
                list_Jobs.Location = list_Jobs_Model.Location;
                list_Jobs.Vacancies = list_Jobs_Model.Vacancies;
                list_Jobs.Description = list_Jobs_Model.Description;
                entities.List_Jobs.Add(list_Jobs);
                entities.SaveChanges();
                return list_Jobs_Model;
            }
        }
        //--Update Job--\\
        public Job_Model UpdateJob(Job_Model list_Jobs_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var jobUpdate = entities.List_Jobs.FirstOrDefault(job => job.ID == list_Jobs_Model.ID);
                jobUpdate.Location = list_Jobs_Model.Location;
                jobUpdate.Team = list_Jobs_Model.Team;
                jobUpdate.Vacancies = list_Jobs_Model.Vacancies;
                jobUpdate.Description = list_Jobs_Model.Description;
                entities.SaveChanges();
                return list_Jobs_Model;
            }
        }
        //--Delete--\\
        public Job_Model Delete(Job_Model list_Jobs)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                Job_Model list_Jobs_Model = new Job_Model();
                var job = entities.List_Jobs.FirstOrDefault(x => x.ID == list_Jobs.ID);
                entities.List_Jobs.Remove(job);
                entities.SaveChanges();
                return list_Jobs_Model;
            }
        }
    }
}