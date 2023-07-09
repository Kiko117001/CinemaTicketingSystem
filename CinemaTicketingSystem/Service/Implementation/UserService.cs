using CinemaTicketingSystem.Domain;
using CinemaTicketingSystem.Domain.DomainModels;
using CinemaTicketingSystem.Domain.DTO;
using CinemaTicketingSystem.Domain.Entities;
using CinemaTicketingSystem.Domain.Identity;
using CinemaTicketingSystem.Domain.Relations;
using CinemaTicketingSystem.Repository.Interface;
using CinemaTicketingSystem.Service.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaTicketingSystem.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<CinemaTicketsAppUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<CinemaTicketsAppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public IEnumerable<CinemaTicketsAppUser> GetAllUsers()
        {
            return this._userRepository.GetAll();
        }

        public void UpdateUserRole(string userId, List<UserRoles> model)
        {
            var user = _userRepository.Get(userId);

            List<string> roles = new List<string>(model.Where(z => z.IsSelected).Select(y => y.RoleName));

            user.Role = roles.SingleOrDefault();

            _userRepository.Update(user);
        }
    }
}
