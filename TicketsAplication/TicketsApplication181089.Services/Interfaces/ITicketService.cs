using System;
using System.Collections.Generic;
using System.Text;
using TicketsApplication.Domain.DomainModels;
using TicketsApplication.Domain.DTO;

namespace TicketsApplication.Services.Interfaces
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket p);
        void UpdeteExistingTicket(Ticket p);
        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
    }
}
