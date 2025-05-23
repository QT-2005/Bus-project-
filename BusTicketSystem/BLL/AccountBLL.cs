using BusTicketSystem.DAL;
using BusTicketSystem.DTO;
using System.Collections.Generic;
using System.Security.Principal;

namespace BusTicketSystem.BLL
{
    public class AccountBLL
    {
        private readonly AccountDAL _accountDAL;

        public AccountBLL()
        {
            _accountDAL = new AccountDAL();
        }

        public Account Login(string username, string password)
        {
            var account = _accountDAL.GetAccountByUsername(username);
            if (account != null && account.Password == password && account.IsActive)
            {
                return account;
            }
            return null;
        }

        public List<Account> GetAllAccounts()
        {
            return _accountDAL.GetAllAccounts();
        }

        public List<Account> SearchAccounts(FilterAccount filter)
        {
            return _accountDAL.GetAccountsByFilter(filter);
        }

        public bool AddAccount(Account account)
        {
            // Check if username already exists
            var existingAccount = _accountDAL.GetAccountByUsername(account.Username);
            if (existingAccount != null)
            {
                return false;
            }

            return _accountDAL.AddAccount(account);
        }

        public bool UpdateAccount(Account account)
        {
            // Check if username already exists (except for this account)
            var existingAccount = _accountDAL.GetAccountByUsername(account.Username);
            if (existingAccount != null && existingAccount.Id != account.Id)
            {
                return false;
            }

            return _accountDAL.UpdateAccount(account);
        }

        public bool DeleteAccount(int id)
        {
            return _accountDAL.DeleteAccount(id);
        }

        public bool ChangePassword(int accountId, string oldPassword, string newPassword)
        {
            var account = _accountDAL.GetAccountByUsername(Config.Session.CurrentUser.Username);
            if (account != null && account.Password == oldPassword)
            {
                account.Password = newPassword;
                return _accountDAL.UpdateAccount(account);
            }
            return false;
        }
    }
}
