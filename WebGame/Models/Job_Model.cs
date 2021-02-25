using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class Job_Model
    {
        public int ID { get; set; }
        public string Team { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Vacancies { get; set; }
    }
}