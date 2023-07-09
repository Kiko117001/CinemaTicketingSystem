using CinemaTicketingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Domain.Relations
{
    public class TicketInShoppingCart : BaseEntity
    {
        public Guid TicketId { get; set; }
        public virtual Ticket CurrentTicket { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

        public int Quantity { get; set; }
    }
}
