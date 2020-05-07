using System.Linq;

namespace BeerApi
{
    public interface IOrderServiceApp
    {
        Order GetOrderFromRequest(NewOrderRequest req);
    }

    class OrderServiceApp : IOrderServiceApp 
    {
        private readonly BeersDbContext _ctx;
        public OrderServiceApp(BeersDbContext ctx) {
            _ctx = ctx;
        }
        public Order GetOrderFromRequest(NewOrderRequest req) {
            
            Order order = new Order();
            
            foreach (var line in req.Items) {
                order.AddOrderLine (
                    _ctx.Beers.FirstOrDefault(b => b.Id == line.BeerId),
                    line.Qty);
            }

            return order;
        }
    }
}