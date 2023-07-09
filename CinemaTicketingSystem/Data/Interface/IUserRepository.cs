using CinemaTicketingSystem.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<CinemaTicketsAppUser> GetAll();
        CinemaTicketsAppUser Get(string id);
        void Insert(CinemaTicketsAppUser entity);
        void Update(CinemaTicketsAppUser entity);
        void Delete(CinemaTicketsAppUser entity);
    }
}
