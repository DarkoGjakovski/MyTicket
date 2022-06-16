using System;
using System.Collections.Generic;
using System.Text;
using TicketsApplication.Domain.Identity;

namespace TicketsApplication.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<TicketsApplicationUser> GetAll();
        TicketsApplicationUser Get(string id);
        void Insert(TicketsApplicationUser entity);
        void Update(TicketsApplicationUser entity);
        void Delete(TicketsApplicationUser entity);
    }
}
