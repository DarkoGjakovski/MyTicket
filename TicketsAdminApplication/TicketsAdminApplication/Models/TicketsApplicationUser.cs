using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsApplication.Domain.DomainModels;

namespace TicketsAdminApplication.Models
{
    public class TicketsApplicationUser
    {
        public string Email { get; set; }
     
        public string NormalizedUserName { get; set; }
      
        public string UserName { get; set; }
        public EnumRoles Role { get; set; }
    }
}
