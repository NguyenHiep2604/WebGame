using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class List_Game_DB
    {
        public List<List_Game_Model> ListAllGame()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listGame = (from game in entities.List_Game
                                select new List_Game_Model
                                {
                                    ID = game.ID,
                                    Image = game.Image,
                                    Name = game.Name,
                                    LinkIOS = game.LinkIOS,
                                    LinkAndroid = game.LinkAndroid,
                                    Caption = game.Caption
                                }).OrderByDescending(game => game.ID).ToList();
                return listGame;
            }
        }
        //--Add New--\\
        public List_Game_Model AddNew(List_Game_Model list_Game_Model)
        {
            using(WebGameEntities entities = new WebGameEntities())
            {
                List_Game list_Game = new List_Game();
                list_Game.Name = list_Game_Model.Name;
                list_Game.LinkIOS = list_Game_Model.LinkIOS;
                list_Game.LinkAndroid = list_Game_Model.LinkAndroid;
                list_Game.Caption = list_Game_Model.Caption;
                list_Game.Featured_Games = list_Game_Model.Featured_Games;
                list_Game.Image = list_Game_Model.Image;
                entities.List_Game.Add(list_Game);
                entities.SaveChanges();

                return list_Game_Model;
            }
        }
        //--Update--\\
        public List_Game_Model Update(List_Game_Model list_Game_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var list_Game = entities.List_Game.FirstOrDefault(s => s.ID == list_Game_Model.ID);
                list_Game.Name = list_Game_Model.Name;
                list_Game.LinkIOS = list_Game_Model.LinkIOS;
                list_Game.LinkAndroid = list_Game_Model.LinkAndroid;
                list_Game.Caption = list_Game_Model.Caption;
                list_Game.Featured_Games = list_Game_Model.Featured_Games;
                list_Game.Image = list_Game_Model.Image;
                entities.SaveChanges();

                return list_Game_Model;
            }
        }
        //--Delete--\\
        public List_Game_Model Delete(int id)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                List_Game_Model list_Game = new List_Game_Model();
                var game = entities.List_Game.FirstOrDefault(x => x.ID == id);
                entities.List_Game.Remove(game);
                entities.SaveChanges();
                return list_Game;
            }
        }
    }
}