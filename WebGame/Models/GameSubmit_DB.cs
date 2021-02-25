using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class GameSubmit_DB
    {
        public GameSubmit_Model GameSubmit(GameSubmit_Model gameSubmit_Model)
        {
            using(WebGameEntities entities = new WebGameEntities())
            {
                GameSubmit gameSubmit = new GameSubmit();
                gameSubmit.FullName = gameSubmit_Model.FullName;
                gameSubmit.Email = gameSubmit_Model.Email;
                gameSubmit.GameTitle = gameSubmit_Model.GameTitle;
                gameSubmit.VideoFootageLink = gameSubmit_Model.VideoFootageLink;
                gameSubmit.CompanyName = gameSubmit_Model.CompanyName;
                gameSubmit.Country = gameSubmit_Model.Country;
                gameSubmit.LinkAppStore = gameSubmit_Model.LinkAppStore;
                gameSubmit.MoreAbout = gameSubmit_Model.MoreAbout;
                gameSubmit.DateSubmit = gameSubmit_Model.DateSubmit;

                entities.GameSubmits.Add(gameSubmit);
                entities.SaveChanges();

                return gameSubmit_Model;
            }
        }

        public List<GameSubmit_Model> ListAll()
        {
            using(WebGameEntities entities = new WebGameEntities())
            {
                var list = (from data in entities.GameSubmits
                            select new GameSubmit_Model
                            {
                                ID = data.ID,
                                FullName = data.FullName,
                                Email = data.Email,
                                GameTitle = data.VideoFootageLink,
                                VideoFootageLink = data.VideoFootageLink,
                                CompanyName = data.CompanyName,
                                Country = data.Country,
                                LinkAppStore = data.LinkAppStore,
                                MoreAbout = data.MoreAbout,
                                DateSubmit = data.DateSubmit
                            }).OrderByDescending(n => n.ID).ToList();
                return list;
            }
        }

        public GameSubmit_Model Delete(GameSubmit_Model gameSubmit)
        {
            using(WebGameEntities entities = new WebGameEntities())
            {
                GameSubmit_Model gameSubmit_Model = new GameSubmit_Model();
                var delete = entities.GameSubmits.FirstOrDefault(x => x.ID == gameSubmit.ID);
                entities.GameSubmits.Remove(delete);
                entities.SaveChanges();
                return gameSubmit_Model;
            }
        }
    }
}