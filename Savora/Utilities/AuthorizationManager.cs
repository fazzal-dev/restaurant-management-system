using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savora.Utilities
{
    public class AuthorizationManager
    {
        public const string AdminRole = "Admin";
        public const string ManagerRole = "Manager";
        public const string WaiterRole = "Waiter";
        public const string CashierRole = "Cashier";

        public static bool IsAuthorized(string userRole, string action)
        {
            switch (userRole)
            {
                case AdminRole:
                    // Admin can do everything
                    return true;

                case ManagerRole:
                    // Manager can add, update, and remove tables
                    return action == "AddTable" || action == "AddCatgory" || action == "AddProduct";

                case WaiterRole:
                    // Waiter can see kitchen items
                    return action == "Kitchen";

                case CashierRole:
                    // Cashier can do POS
                    return action == "POS";

                default:
                    // Default to not authorized for unknown roles
                    return false;
            }
        }
    }
}
