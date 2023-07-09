using CinemaTicketingSystem.Domain;
using CinemaTicketingSystem.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketingSystem.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public CinemaTicketsAppUser User { get; set; }
        public string UserId { get; set; }


        public virtual ICollection<TicketInOrder> TicketsInOrder { get; set; }
    }
}
