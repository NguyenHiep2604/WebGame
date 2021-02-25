using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class GameSubmit_Model
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string GameTitle { get; set; }
        public string VideoFootageLink { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string LinkAppStore { get; set; }
        public string MoreAbout { get; set; }
        public System.DateTime DateSubmit { get; set; }
    }
}