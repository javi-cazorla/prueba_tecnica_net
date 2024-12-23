using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_net.Entidades;
using prueba_tecnica_net.Services;

namespace prueba_tecnica_net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BanksController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ESettService _eSettService = new ESettService();
        public BanksController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBanksFromESett")]
        public IEnumerable<Bank> Get()
        {
            return new [] { new Bank() };
        }

        [HttpPost(Name = "SaveBanksInDb")]
        public IEnumerable<Bank> SaveBanksInDb()
        {
            var banks = _eSettService.GetBanksData();

            return banks;
        }
    }
}
