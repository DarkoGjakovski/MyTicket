
using System;
using System.Collections.Generic;
using System.Text;
using TicketsApplication.Domain.DTO;

namespace TicketsApplication.Services.Interfaces
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromShoppingCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}
