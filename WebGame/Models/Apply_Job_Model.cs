using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class Apply_Job_Model
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CurrentCompany { get; set; }
        public byte[] CV { get; set; }
        public string LinkedInURL { get; set; }
        public string TwitterURL { get; set; }
        public string GithubURL { get; set; }
        public string PortfolioURL { get; set; }
        public string OtherWebsite { get; set; }
        public string More { get; set; }
    }
}