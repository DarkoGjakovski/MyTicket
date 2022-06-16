
using System;
using System.Collections.Generic;
using System.Text;
using TicketsApplication.Domain.DomainModels;

namespace TicketsApplication.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> getAllOrders();

        Order getOrderDetails(BaseEntity model);
    }
}
