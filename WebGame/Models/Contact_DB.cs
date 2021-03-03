using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class Contact_DB
    {
        public Contact_Model AddContact(Contact_Model contact_Model)
        {
            using(WebGameEntities entities = new WebGameEntities())
            {
                Contact contact = new Contact();
                contact.FullName = contact_Model.FullName;
                contact.Email = contact_Model.Email;
                contact.Subject = contact_Model.Subject;
                contact.Optional = contact_Model.Game.ID;
                contact.Message = contact_Model.Message;
                entities.Contacts.Add(contact);
                entities.SaveChanges();
                return contact_Model;
            }
        }

        public List<Contact_Model> ListAll()
        {
            using(WebGameEntities entities = new WebGameEntities())
            {
                var listContact = (from t in entities.Contacts
                                   join a in entities.List_Game on t.Optional equals a.ID
                                   select new Contact_Model
                                   {
                                       ID = t.ID,
                                       FullName = t.FullName,
                                       Email = t.Email,
                                       Message = t.Message,
                                       Subject = t.Subject,
                                       Game = new Game_Model()
                                       {
                                           ID = a.ID,
                                           NameGame = a.Name
                                       }
                                   }).OrderByDescending(t => t.ID).ToList();
                return listContact;
            }
        }

        public Contact_Model Delete(Contact_Model contact)
        {
            Contact_Model contact_Model = new Contact_Model();
            using(WebGameEntities entities = new WebGameEntities())
            {
                var contactDelete = entities.Contacts.FirstOrDefault(x => x.ID == contact.ID);
                entities.Contacts.Remove(contactDelete);
                entities.SaveChanges();
            }
            return contact_Model;
        }
    }
}