using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsApplication.Domain.DomainModels;

namespace TicketsAdminApplication.Models
{
    public class Ticket
    {
       
        public string TicketName { get; set; }
        
        public string TicketImage { get; set; }
       
        public string TicketDescription { get; set; }
      
        public DateTime TicketDate { get; set; }

     
        public int TicketPrice { get; set; }
       
        public EnumGenre Genre { get; set; }
        public int TicketRating { get; set; }
    }
}
