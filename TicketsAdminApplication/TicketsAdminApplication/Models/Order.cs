﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsAdminApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public TicketsApplicationUser User { get; set; }

        public IEnumerable<TicketInOrder> TicketInOrders { get; set; }
    }
}
