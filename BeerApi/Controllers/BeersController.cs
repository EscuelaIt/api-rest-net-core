using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeersController : ControllerBase
    {

        private readonly ILogger<BeersController> _logger;
        private readonly IBeerService _beersService;

        private readonly BeersConfig _config;


        public BeersController(ILogger<BeersController> logger, IBeerService beersService, IOptions<BeersConfig> bc)
        {
            _logger = logger;
            _beersService = beersService;
            _config = bc.Value;
        }

        [HttpGet]
        public IEnumerable<Beer> Get()
        {
            return _beersService.GetAll();  
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) 
        {
            var beer = _beersService.FindById(id);
            return beer != null ? Ok(beer) as IActionResult : NotFound();
        }

        [HttpGet("{name:alpha}")]
        public IActionResult  GetByName(string name) {
            name= name.ToUpper();
            return Ok(_beersService.FindByName(name));
        }

        [HttpPost()]
        public IActionResult Create(Beer beer) {
            var result = _beersService.TryAdd( beer);

            if (result) {
                return Ok(beer);
            }
            else {
                return BadRequest();
            }
        }
    }
}
