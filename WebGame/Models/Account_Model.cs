using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class Account_Model
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
    }
}