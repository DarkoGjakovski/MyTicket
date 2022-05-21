using MyTicket.Domain.Idenitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTicket.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<MyTicketApplicationUser> GetAll();
        MyTicketApplicationUser Get(string id);
        void Insert(MyTicketApplicationUser entity);
        void Update(MyTicketApplicationUser entity);
        void Delete(MyTicketApplicationUser entity);
    }
}
