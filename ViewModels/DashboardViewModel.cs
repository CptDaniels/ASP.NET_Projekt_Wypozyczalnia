using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ASP.NET_Projekt_Wypozyczalnia.ViewModels
{
    public class DashboardViewModel
    {
        public int CarCount { get; set; }
        public int ClientCount { get; set; }

        public List<IdentityUser> Users { get; set; }
        public List<string> Roles { get; set; }

        public string NewUserEmail { get; set; }
        public string NewUserPassword { get; set; }

        public string SelectedUserId { get; set; }
        public string SelectedRole { get; set; }

        public Dictionary<string, List<string>> UserRoles { get; set; } = new Dictionary<string, List<string>>();
    }
}
