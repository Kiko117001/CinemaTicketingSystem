using CinemaTicketingSystem.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto GetShoppingCartInfo(string userId);
        bool DeleteProductFromSoppingCart(string userId, Guid ticketId);
        bool Order(string userId);
    }
}
