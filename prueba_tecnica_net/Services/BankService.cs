using Newtonsoft.Json;
using prueba_tecnica_net.Entidades;
using prueba_tecnica_net.Enumerables;
using prueba_tecnica_net.Interfaces;
using prueba_tecnica_net.Persistencia;

namespace prueba_tecnica_net.Services
{
    public class BankService(ApplicationDbContext dbContext) : IBankService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //Devuelve todos los bancos de la base de datos
        public IEnumerable<Bank> GetAllBanks()
        {
            return _dbContext.Banks;
        }

        public Bank GetBankById(int id)
        {
            return _dbContext.Banks.Find(id);
        }

        //Devuelve un banco por su nombre
        //@param name: nombre del banco
        public Bank GetBankByName(string name)
        {
            return _dbContext.Banks.FirstOrDefault(bank => bank.Name == name);
        }

        //Devuelve todos los bancos con el nombre especificado
        //@param name: nombre del banco
        public IEnumerable<Bank> GetBanksByName(string name)
        {
            return _dbContext.Banks.Where(bank => bank.Name.Contains(name));
        }

        //Guarda los bancos en la base de datos
        //@param banks: lista de bancos a guardar
        public SavedStatus SaveBanks(IEnumerable<Bank> banks)
        {
            if (banks.Any())
            {
                try
                {
                    _dbContext.Banks.AddRange(banks);
                    _dbContext.SaveChanges();

                    return SavedStatus.Saved;
                }
                catch (Exception)
                {
                    return SavedStatus.Error;
                }
            }

            return SavedStatus.Empty;
        }

        //Elimina los bancos de la base de datos
        //@param banks: lista de bancos a eliminar
        public DeletedStatus DeleteBanks(IEnumerable<Bank> banks)
        {
            if (banks.Any())
            {
                try
                {
                    _dbContext.Banks.RemoveRange(banks);
                    _dbContext.SaveChanges();

                    return DeletedStatus.Deleted;
                }
                catch (Exception)
                {
                    return DeletedStatus.Error;
                }
            }

            return DeletedStatus.Empty;
        }
    }
}
