using CinemaTicketingSystem.Domain;
using CinemaTicketingSystem.Domain.DomainModels;
using CinemaTicketingSystem.Repository.Interface;
using CinemaTicketingSystem.Service.Interface;
using CinemaTicketingSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }
     
        public List<Order> GetAllOrders(string userId)
        {
                var loggedInUser = this._userRepository.Get(userId).Id;
                return this._orderRepository.GetAllOrders(loggedInUser);
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return this._orderRepository.GetOrderDetails(model);
        }
    }
}
