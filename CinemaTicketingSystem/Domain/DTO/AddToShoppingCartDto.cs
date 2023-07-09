using CinemaTicketingSystem.Domain.Entities;
using CinemaTicketingSystem.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Ticket SelectedTicket { get; set; }
        public Guid SelectedTicketId { get; set; }
        public int Quantity { get; set; }
    }
}
