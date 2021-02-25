using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class GroupJob
    {
        public string GroupName { get; set; }
        public List<Job_Model> Jobs { get; set; }
    }
}