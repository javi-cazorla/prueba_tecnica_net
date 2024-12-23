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
                return new { StatusCode = SavedStatus.NotSaved, Message = "Ya existen datos de bancos en la base de datos, no se van a guardar más." };

            List<Bank> banks = _eSettService.GetBanksData();

            if (banks.Count > 0)
            {
                try
                {
                    _dbContext.Banks.AddRange(banks);
                    _dbContext.SaveChanges();
                    return new { StatusCode = SavedStatus.Saved, Message = "Se han guardado los datos de los bancos recibidos por la API en la base de datos." };
                }
                catch (Exception)
                {
                    return new { StatusCode = SavedStatus.Error, Message = "Ha ocurrido un error al guardar los bancos en la base de datos." };
                }
            }

            return new { StatusCode = SavedStatus.Empty, Message = "No hemos recibido datos de la API de eSett, no se ha guardado nada." };
        }

        [HttpDelete(Name = "RemoveBanksFromDb")]
        public object RemoveBanksFromDb()
        {
            IEnumerable<Bank> banks = _dbContext.Banks;

            if (banks.Any())
            {
                _dbContext.Banks.RemoveRange(banks);
                _dbContext.SaveChanges();
                return new { StatusCode = DeletedStatus.Deleted, Message = "Se han borrado todos los bancos de la base de datos." };
            }

            return new { StatusCode = DeletedStatus.Empty, Message = "No se han borrado datos de la base de datos porque estaba vacía." };
        }
    }
}
