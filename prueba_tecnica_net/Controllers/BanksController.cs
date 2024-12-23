using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_net.Entidades;
using prueba_tecnica_net.Enumerables;
using prueba_tecnica_net.Persistencia;
using prueba_tecnica_net.Services;

namespace prueba_tecnica_net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BanksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ESettService _eSettService = new ESettService();
        
        public BanksController(ILogger<WeatherForecastController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetBanksFromESett")]
        public IEnumerable<Bank> Get()
        {
            IEnumerable<Bank> savedBanks = _dbContext.Banks;
            return savedBanks;
        }

        [HttpPost(Name = "SaveBanksInDb")]
        public object SaveBanksInDb()
        {
            List<Bank> savedBanks = _dbContext.Banks.ToList();

            if (savedBanks.Count > 0)
                return new { StatusCode = ResponseStatus.NotSaved, Message = "Ya existen datos de bancos en la base de datos, no se van a guardar más." };

            List<Bank> banks = _eSettService.GetBanksData();

            if (banks.Count > 0)
            {
                try
                {
                    _dbContext.Banks.AddRange(banks);
                    _dbContext.SaveChanges();
                    return new { StatusCode = ResponseStatus.Saved, Message = "Se han guardado los datos de los bancos recibidos por la API en la base de datos." };
                }
                catch (Exception)
                {
                    return new { StatusCode = ResponseStatus.Error, Message = "Ha ocurrido un error al guardar los bancos en la base de datos." };
                }
            }

            return new { StatusCode = ResponseStatus.Empty, Message = "No hemos recibido datos de la API de eSett, no se ha guardado nada." };
        }
    }
}
