
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsApplication.Domain.Identity;

namespace TicketsApplication.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public TicketsApplicationUser User { get; set; }

        public IEnumerable<TicketInOrder> TicketInOrders { get; set; }
    }
}
