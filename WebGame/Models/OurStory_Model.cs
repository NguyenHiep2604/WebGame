using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class OurStory_Model
    {
        public int ID { get; set; }
        public byte[] PictureMaxWidth { get; set; }
        public byte[] PictureWidth640 { get; set; }
        public string Title { get; set; }
        public string OurStoryName { get; set; }
    }
}