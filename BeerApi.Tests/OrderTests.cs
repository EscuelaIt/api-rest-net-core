using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BeerApi.Tests
{
    public class GivenOrder
    {
        [Fact]
        public void AddOrderLineWithExistingProductIncrementsQtyOfExistingOrderLine()
        {
            var order = new Order();

            var beer = new Beer() {
                Name = "test beer",
                Price = 2,
                Abv = 1.3f
            };
            order.AddOrderLine(beer, 10);
            order.AddOrderLine(beer, 12);

            var line = order.Lines.First();
            Assert.Equal(10+12, line.Qty );
        }

        [Fact]
        public void AddOrderLineWithExistingProductShouldNotCreateANewOrderLine()
        {
            var order = new Order();
            var beer = new Beer() {
                Name = "test beer",
                Price = 2,
                Abv = 1.3f
            };

            order.AddOrderLine(beer, 10);
            order.AddOrderLine(beer, 12);   

            Assert.Equal(1, order.Lines.Count());
        }

        [Fact]
        public void AddOrderLineWithDifferentProductShouldCreateANewOrderLine()
        {
            var order = new Order();
            var beer = new Beer() {
                Name = "test beer",
                Price = 2,
                Abv = 1.3f
            };
            order.AddOrderLine(beer, 10);
            var beer2 = new Beer()
            {
                Id = 1,
                Name = "test beer",
                Price = 2,
                Abv = 1.3f                
            };
            order.AddOrderLine(beer2, 5);

            Assert.Equal(2, order.Lines.Count());
        }
    }

    public class GivenBeersService 
    {
        [Fact]
        public void FindByIdShouldReturnBeerWithSameIdIfExists()
        {
            
            var context = new BeersDbContext(new DbContextOptions<BeersDbContext>());
            context.Beers.Add(new Beer() {
                Id= 2,
                Name = "A",
                Price = 0,
                Abv = 0
            });
            
            var mock = new Mock<BeersDbContext>();
            mock.Setup(ctx => ctx.Beers).Returns(context.Beers);

            var svc = new BeerService(null, mock.Object);
            var beer = svc.FindById(2);
            Assert.Equal(2, beer.Id);
        }

    }
}
