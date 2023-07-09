using CinemaTicketingSystem.Domain.DomainModels;
using CinemaTicketingSystem.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Service.Interface
{
    public interface IUserService
    {
        public IEnumerable<CinemaTicketsAppUser> GetAllUsers();
        public void UpdateUserRole(string userId, List<UserRoles> model);
    }
}
