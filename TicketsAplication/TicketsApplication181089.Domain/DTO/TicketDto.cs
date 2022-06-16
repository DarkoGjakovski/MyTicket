using System;
using System.Collections.Generic;
using System.Text;
using TicketsApplication.Domain.DomainModels;

namespace TicketsApplication.Domain.DTO
{
   public class TicketDto
    {
        public List<Ticket> Tickets { get; set; }
        public DateTime Date { get; set; }
        public EnumGenre? Genre { get; set; }
        public EnumRoles? CurrentUserRole { get; set; }
    }
}
