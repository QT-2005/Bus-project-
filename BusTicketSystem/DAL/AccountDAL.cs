using BusTicketSystem.Config;
using BusTicketSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

namespace BusTicketSystem.DAL
{
    public class AccountDAL
    {
        public Account GetAccountByUsername(string username)
        {
            using (var context = new BusTicketSystemDB())
            {
                return context.Accounts
                    .FirstOrDefault(a => a.Username == username);
            }
        }

        public List<Account> GetAllAccounts()
        {
            using (var context = new BusTicketSystemDB())
            {
                return context.Accounts.ToList();
            }
        }

        public List<Account> GetAccountsByFilter(FilterAccount filter)
        {
            using (var context = new BusTicketSystemDB())
            {
                var query = context.Accounts.AsQueryable();

                if (!string.IsNullOrEmpty(filter.Username))
                {
                    query = query.Where(a => a.Username.Contains(filter.Username));
                }

                if (!string.IsNullOrEmpty(filter.FullName))
                {
                    query = query.Where(a => a.FullName.Contains(filter.FullName));
                }

                if (!string.IsNullOrEmpty(filter.Email))
                {
                    query = query.Where(a => a.Email.Contains(filter.Email));
                }

                if (!string.IsNullOrEmpty(filter.Role))
                {
                    query = query.Where(a => a.Role == filter.Role);
                }

                if (filter.IsActive.HasValue)
                {
                    query = query.Where(a => a.IsActive == filter.IsActive.Value);
                }

                return query.ToList();
            }
        }

        public bool AddAccount(Account account)
        {
            try
            {
                using (var context = new BusTicketSystemDB())
                {
                    account.CreatedDate = DateTime.Now;
                    context.Accounts.Add(account);
                    return context.SaveChanges() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAccount(Account account)
        {
            try
            {
                using (var context = new BusTicketSystemDB())
                {
                    context.Entry(account).State = EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAccount(int id)
        {
            try
            {
                using (var context = new BusTicketSystemDB())
                {
                    var account = context.Accounts.Find(id);
                    if (account != null)
                    {
                        context.Accounts.Remove(account);
                        return context.SaveChanges() > 0;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
