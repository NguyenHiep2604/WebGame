using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGame.Models
{
    public class AccountDB
    {
        //--List All Account--\\
        public List<Account_Model> ListAllAccount()
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var listAccount = (from account in entities.Accounts
                                   select new Account_Model
                                   {
                                       ID = account.ID,
                                       Email = account.Email,
                                       PassWord = account.PassWord,
                                       UserName = account.UserName
                                   }).OrderByDescending(account => account.ID).ToList();
                return listAccount;
            }
        }
        //--Create Account--\\
        public Account_Model CreateAccount(Account_Model account_Model)
        {
            using(WebGameEntities entities = new WebGameEntities())
            {
                Account account = new Account();
                account.Email = account_Model.Email;
                account.PassWord = account_Model.PassWord;
                account.UserName = account_Model.UserName;
                entities.Accounts.Add(account);
                entities.SaveChanges();

                return account_Model;
            }
        }
        //--Update Account--\\
        public Account_Model UpdateAccount(Account_Model account_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                var account = entities.Accounts.FirstOrDefault(x => x.ID == account_Model.ID);
                account.Email = account_Model.Email;
                account.PassWord = account_Model.PassWord;
                account.UserName = account_Model.UserName;
                entities.SaveChanges();

                return account_Model;
            }
        }
        //--Delete Account--\\
        public Account_Model DeleteAccount(Account_Model account_Model)
        {
            using (WebGameEntities entities = new WebGameEntities())
            {
                Account_Model accountList = new Account_Model();
                var accountDelete = entities.Accounts.FirstOrDefault(x => x.ID == account_Model.ID);
                entities.Accounts.Remove(accountDelete);
                entities.SaveChanges();
                return accountList;
            }
        }
    }
}