using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_net.Entidades;
using prueba_tecnica_net.Enumerables;
using prueba_tecnica_net.Interfaces;
using prueba_tecnica_net.Persistencia;
using prueba_tecnica_net.Services;

namespace prueba_tecnica_net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BanksController : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ESettService _eSettService = new ESettService();

        public BanksController(ApplicationDbContext dbContext, IBankService bankService)
        {
            _dbContext = dbContext;
            _bankService = bankService;
        }

        [HttpGet(Name = "GetAllBanks")]
        public IEnumerable<Bank> GetAll()
        {
            return _bankService.GetAllBanks();
            //IEnumerable<Bank> savedBanks = _dbContext.Banks;
            //return savedBanks;
        }

        [HttpGet("id/{id}", Name = "GetBankById")]
        public object GetById(int id)
        {
            var res = _bankService.GetBankById(id);

            if (res is not null)
                return res;

            return new {Message = "No se ha encontrado un banco con ese id"};
        }

        [HttpGet("name/{name}", Name = "GetBankByName")]
        public object GetBankByName(string name)
        {
            var res = _bankService.GetBankByName(name);

            if (res is not null)
                return res;

            return new { Message = "No se ha encontrado un banco con ese nombre" };
        }

        [HttpGet("banks/name/{name}", Name = "GetBanksByName")]
        public object GetBanksByName(string name)
        {
            var res = _bankService.GetBanksByName(name);

            if (res is not null)
                return res;

            return new { Message = "No se ha encontrado ningún banco con ese nombre" };

            //IEnumerable<Bank> savedBanks = _dbContext.Banks;
            //return savedBanks;
        }

        [HttpPost(Name = "SaveBanksInDb")]
        public object SaveBanksInDb()
        {
            string message = string.Empty;
            SavedStatus savedStatus = SavedStatus.Empty;

            var savedBanks = _dbContext.Banks;

            if (savedBanks.Any())
                return new { StatusCode = SavedStatus.NotSaved, Message = "Ya existen datos de bancos en la base de datos, no se van a guardar más." };

            IEnumerable<Bank> banks = _eSettService.GetBanksData();

            var status = _bankService.SaveBanks(banks);

            switch (status)
            {
                case SavedStatus.Saved:
                    savedStatus = SavedStatus.Saved;
                    message = "Se han guardado los datos de los bancos recibidos por la API en la base de datos.";
                    break;
                case SavedStatus.Empty:
                    savedStatus = SavedStatus.Empty;
                    message = "No hemos recibido datos de la API de eSett, no se ha guardado nada.";
                    break;
                case SavedStatus.Error:
                    savedStatus = SavedStatus.Error;
                    message = "Ha ocurrido un error al guardar los bancos en la base de datos.";
                    break;
            }

            return new { StatusCode = savedStatus, Message = message };
        }

        [HttpDelete(Name = "RemoveBanksFromDb")]
        public object RemoveBanksFromDb()
        {
            string message = string.Empty;
            DeletedStatus deletedStatus = DeletedStatus.Empty;

            var status = _bankService.DeleteBanks(_dbContext.Banks);

            switch (status)
            {
                case DeletedStatus.Deleted:
                    deletedStatus = DeletedStatus.Deleted;
                    message = "Se han borrado todos los bancos de la base de datos.";
                    break;
                case DeletedStatus.Empty:
                    deletedStatus = DeletedStatus.Empty;
                    message = "No se han borrado datos de la base de datos porque estaba vacía.";
                    break;
                case DeletedStatus.Error:
                    deletedStatus = DeletedStatus.Error;
                    message = "Ha ocurrido un error al borrar los bancos de la base de datos.";
                    break;
            }

            return new { StatusCode = deletedStatus, Message = message };
        }
    }
}
