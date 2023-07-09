using CinemaTicketingSystem.Domain;
using CinemaTicketingSystem.Domain.DomainModels;
using CinemaTicketingSystem.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> GetAllOrders(string userId);
        public Order GetOrderDetails(BaseEntity model);
    }
}
