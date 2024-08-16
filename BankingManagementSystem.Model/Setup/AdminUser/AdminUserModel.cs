using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystem.Models.Setup.AdminUser
{
    public class AdminUserModel
    {
        public int AdminUserId { get; set; }

        public string? AdminUserCode { get; set; } = null!;

        public string AdminUserName { get; set; } = null!;

        public string MobileNo { get; set; } = null!;

        public string UserRoleCode { get; set; } = null!;
    }
}
