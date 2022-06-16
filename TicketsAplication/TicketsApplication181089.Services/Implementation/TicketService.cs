using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketsApplication.Domain.DomainModels;
using TicketsApplication.Domain.DTO;
using TicketsApplication.Repository.Interfaces;
using TicketsApplication.Services.Interfaces;

namespace TicketsApplication.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _TicketRepository;
        private readonly IRepository<TicketInShoppingCart> _TicketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TicketService> _logger;
        public TicketService(IRepository<Ticket> TicketRepository, ILogger<TicketService> logger, IRepository<TicketInShoppingCart> TicketInShoppingCartRepository, IUserRepository userRepository)
        {
            _TicketRepository = TicketRepository;
            _userRepository = userRepository;
            _TicketInShoppingCartRepository = TicketInShoppingCartRepository;
            _logger = logger;
        }

        public bool AddToShoppingCart(AddToShoppingCardDto item, string userID)
        {

            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.TicketId != null && userShoppingCard != null)
            {
                var Ticket = this.GetDetailsForTicket(item.TicketId);

                if (Ticket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = Ticket,
                        TicketId = Ticket.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._TicketInShoppingCartRepository.Insert(itemToAdd);
                    _logger.LogInformation("Ticket was successfully added into ShoppingCart");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong. TicketId or UserShoppingCard may be unaveliable!");
            return false;
        }

        public void CreateNewTicket(Ticket p)
        {
            this._TicketRepository.Insert(p);
        }

        public void DeleteTicket(Guid id)
        {
            var Ticket = this.GetDetailsForTicket(id);
            this._TicketRepository.Delete(Ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            _logger.LogInformation("GetAllTickets was called!");
            return this._TicketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return this._TicketRepository.Get(id);
        }

        public AddToShoppingCardDto GetShoppingCartInfo(Guid? id)
        {
            var Ticket = this.GetDetailsForTicket(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedTicket = Ticket,
                TicketId = Ticket.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingTicket(Ticket p)
        {
            this._TicketRepository.Update(p);
        }
    }
}
