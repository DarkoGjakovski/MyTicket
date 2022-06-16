using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketsApplication.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public string TicketName { get; set; }
        [Required]
        public string TicketImage { get; set; }
        [Required]
        public string TicketDescription { get; set; }
        [Required]
        public DateTime TicketDate { get; set; }
      
        [Required]
        public int TicketPrice { get; set; }
        [Required]
        public EnumGenre Genre { get; set; }
        public int TicketRating { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public IEnumerable<TicketInOrder> TicketInOrders { get; set; }

    }
}
