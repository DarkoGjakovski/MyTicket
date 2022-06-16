using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsAdminApplication.Models
{
    public class OrdersDto
    {
        public List<Order> Orders { get; set; }
        public bool IsAdmin { get; set; }
    }
}
