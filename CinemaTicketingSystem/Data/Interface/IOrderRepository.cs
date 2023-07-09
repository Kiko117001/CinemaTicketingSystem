using CinemaTicketingSystem.Domain;
using CinemaTicketingSystem.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> GetAllOrders(string userId);
        public Order GetOrderDetails(BaseEntity model);
    }
}
