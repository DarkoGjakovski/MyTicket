using System;
using System.Collections.Generic;
using System.Text;
using TicketsApplication.Domain.DomainModels;

namespace TicketsApplication.Repository.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> getAllOrders();
        Order getOrderDetails(BaseEntity model);
    }
}
