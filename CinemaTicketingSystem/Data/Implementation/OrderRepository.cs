using CinemaTicketingSystem.Data;
using CinemaTicketingSystem.Domain;
using CinemaTicketingSystem.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsApp.Repository.Implementation
{
    public class OrderRepository : CinemaTicketingSystem.Repository.Interface.IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders(string userId)
        {
            return entities
                .Where(z => z.UserId == userId)
                .Include(z => z.User)
                .Include(z => z.TicketsInOrder)
                .Include("TicketsInOrder.Ticket")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
               .Include(z => z.User)
               .Include(z => z.TicketsInOrder)
               .Include("TicketsInOrder.Ticket")
               .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
