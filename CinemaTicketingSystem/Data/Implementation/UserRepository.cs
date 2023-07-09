using CinemaTicketingSystem.Data;
using CinemaTicketingSystem.Domain.Identity;
using CinemaTicketingSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaTicketsApp.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<CinemaTicketsAppUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<CinemaTicketsAppUser>();
        }

        public CinemaTicketsAppUser Get(string id)
        {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.TicketsInShoppingCart")
               .Include("UserCart.TicketsInShoppingCart.CurrentTicket")
               .SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<CinemaTicketsAppUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(CinemaTicketsAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(CinemaTicketsAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(CinemaTicketsAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
