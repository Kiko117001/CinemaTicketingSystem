using CinemaTicketingSystem.Domain.Relations;
using CinemaTicketingSystem.Domain.Relations;            
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Tickets { get; set; }

        public double TotalPrice { get; set; }

        public static implicit operator ShoppingCartDto(ShoppingCartDto v)
        {
            throw new NotImplementedException();
        }
    }
}
