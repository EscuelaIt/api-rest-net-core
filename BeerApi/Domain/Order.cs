using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerApi
{

    public class Order {
        public Guid Id {get; }

        private List<OrderLine> _lines;
        public IEnumerable<OrderLine>  Lines {get => _lines;}

        public decimal Total 
        {
            get 
            {
                return Lines.Select(b => b.Beer.Price * b.Qty).Sum();
            }
        }

        public Order()
        {
            Id = Guid.NewGuid();
            _lines = new List<OrderLine>();
        }

        public void AddOrderLine (Beer beer, int qty) {
            if (qty == 0) {
                throw new ArgumentException("qty must be greater than 0");
            }

            foreach (var line in _lines) {
                if (line.Beer.Id == beer.Id) {
                    line.Qty += qty;
                    return;
                }
            }
            _lines.Add(new OrderLine() {Beer = beer,Qty = qty});
        }

    }

    public class OrderLine {
        public Beer Beer {get; set;}
        public int Qty {get; set;}

    }
}