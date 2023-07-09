using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Domain.DomainModels
{
    public class UserRoles
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public bool IsSelected { get; set; }
    }
}
