using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using CinemaTicketingSystem.Domain.Relations;

namespace CinemaTicketingSystem.Domain.Entities
{
    public class Ticket
    {
        [Required]
        public string TicketName { get; set; }

        [Required]
        public string TicketImage { get; set; }

        [Required]
        [Display(Name = "Valid till")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public string TicketGenre { get; set; }

        [Required]
        public double TicketPrice { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
        public virtual ICollection<TicketInOrder> TicketsInOrder { get; set; }
        public Guid Id { get; internal set; }
    }
}
