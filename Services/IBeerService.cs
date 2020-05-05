using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BeerApi
{
    public interface IBeerService
    {
        IEnumerable<Beer> GetAll();
        Beer FindById(int id);
        bool TryAdd(int id, Beer beer);
    }

    public class BeerService : IBeerService
    {
        private List<Beer> _beers;
        private readonly ILogger<BeerService> _logger;

        public BeerService(ILogger<BeerService> logger)
        {
            _logger = logger;
            _beers = new List<Beer>();
            _beers.Add(new Beer() {
                Id = 1,
                Name = "Voll Damm",
                Price = 1.75m
            });
            _beers.Add(new Beer() {
                Id = 2,
                Name = "Epidor",
                Price = 2.0m
            });
        }
        public Beer FindById(int id)
        {
            _logger.LogInformation($"Searching for beer with id: {id}");;
            var beer = _beers.SingleOrDefault(b => b.Id == id);
            return beer;
        }

        public IEnumerable<Beer> GetAll()
        {
             return _beers;
        }

        public bool TryAdd(int id, Beer beer)
        {
            if (_beers.Any(b => b.Id == id)) return false;
            _beers.Add(beer);
            return true;
        }
    }
}