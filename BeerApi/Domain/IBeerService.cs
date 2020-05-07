using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerApi
{
    public interface IBeerService
    {
        IEnumerable<Beer> GetAll();
        Beer FindById(int id);
        bool TryAdd(Beer beer);

        IEnumerable<Beer> FindByName(string name);
    }

    public class BeerService : IBeerService
    {        
        private readonly ILogger<BeerService> _logger;
        private readonly BeersDbContext _db;

        public BeerService(ILogger<BeerService> logger, BeersDbContext db)
        {
            _db = db;
            _logger = logger;
        }
        public Beer FindById(int id)
        {
            _logger.LogInformation($"Searching for beer with id: {id}");;
            var beer = _db.Beers.SingleOrDefault(b => b.Id == id);
            return beer;
        }

        public IEnumerable<Beer> FindByName(string name) 
        {
            return _db.Beers
                .Where(b => b.Id > 2)
                .AsEnumerable()
                .Where(b => GetCanonicalName(b.Name) == name).ToList();
        }

        private string GetCanonicalName(string name) {
            return name.ToUpper();
        }

        public IEnumerable<Beer> GetAll()
        {
             return _db.Beers.ToList();
        }

        public bool Delete(int id) 
        {
            var beer = _db.Beers.SingleOrDefault(b => b.Id == id); 
            if (beer != null) {
                _db.Beers.Remove(beer);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool TryAdd(Beer beer)
        {
            beer.Id = 0;
            _db.Beers.Add(beer);
            _db.SaveChanges();
            return true;
        }
    }
}