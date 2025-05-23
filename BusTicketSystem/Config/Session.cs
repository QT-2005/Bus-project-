using BusTicketSystem.DTO;
using System.Security.Principal;

namespace BusTicketSystem.Config
{
    public static class Session
    {
        private static Account _currentUser;

        public static Account CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public static bool IsAdmin
        {
            get { return _currentUser != null && _currentUser.Role == "Admin"; }
        }

        public static bool IsLoggedIn
        {
            get { return _currentUser != null; }
        }

        public static void Logout()
        {
            _currentUser = null;
        }
    }
}
