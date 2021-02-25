using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class List_Game_Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string LinkIOS { get; set; }
        public string LinkAndroid { get; set; }
        public string Caption { get; set; }
        public bool Featured_Games { get; set; }
    }
}