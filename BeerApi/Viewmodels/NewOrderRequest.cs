using System.Collections.Generic;

namespace BeerApi
{
    public class NewOrderRequest 
    {
        public List<OrderLineRequest> Items {get; set;}

    }

    public class OrderLineRequest {
        public int BeerId {get; set;}
        public int Qty {get; set;}

    }
}