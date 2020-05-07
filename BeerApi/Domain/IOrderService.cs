using System;
using System.Linq;

namespace BeerApi {

    public interface IOrderService {
        void ProcessOrder(Order order);
    }

    public class OrderService : IOrderService {
        public OrderService()
        {
        }
        public void ProcessOrder(Order order) 
        {
            Console.WriteLine($"Pedido {order.Id} procesado. Total: {order.Total}.");
        }
    }
}