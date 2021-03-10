using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class OurStory_DB
    {
        //--List All Our Story About Page--\\
        public List<OurStory_Model> ListAllStory()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listStory = (from story in entities.OurStoryAboutPages
                                 select new OurStory_Model
                                 {
                                     ID = story.ID,
                                     Title = story.Title,
                                     OurStoryName = story.OurStoryName
                                 }).OrderByDescending(story => story.ID).ToList();
                return listStory;
            }
        }
        //--Add Our Story--\\
        public OurStory_Model AddNew(OurStory_Model ourStory_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                OurStoryAboutPage ourStoryAboutPage = new OurStoryAboutPage();
                ourStoryAboutPage.PictureMaxWidth = ourStory_Model.PictureMaxWidth;
                ourStoryAboutPage.PictureWidth640 = ourStory_Model.PictureWidth640;
                ourStoryAboutPage.Title = ourStory_Model.Title;
                ourStoryAboutPage.OurStoryName = ourStory_Model.OurStoryName;
                entities.OurStoryAboutPages.Add(ourStoryAboutPage);
                entities.SaveChanges();

                return ourStory_Model;
            }
        }
        //--Update Our Story--\\
        public OurStory_Model Update(OurStory_Model ourStory_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var story = entities.OurStoryAboutPages.FirstOrDefault(s => s.ID == ourStory_Model.ID);
                if(ourStory_Model.PictureMaxWidth != null)
                {
                    story.PictureMaxWidth = ourStory_Model.PictureMaxWidth;
                }
                else
                {
                    story.PictureMaxWidth = story.PictureMaxWidth;
                }

                if(ourStory_Model.PictureWidth640 != null)
                {
                    story.PictureWidth640 = ourStory_Model.PictureWidth640;
                }
                else
                {
                    story.PictureWidth640 = story.PictureWidth640;
                }
                story.Title = ourStory_Model.Title;
                story.OurStoryName = ourStory_Model.OurStoryName;
                entities.SaveChanges();

                return ourStory_Model;
            }
        }
        //--Delete Our Story--\\
        public OurStory_Model Delete(OurStory_Model ourStory_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                OurStory_Model storyList = new OurStory_Model();
                var story = entities.OurStoryAboutPages.FirstOrDefault(x => x.ID == ourStory_Model.ID);
                entities.OurStoryAboutPages.Remove(story);
                entities.SaveChanges();
                return storyList;
            }
        }
    }
}