using prueba_tecnica_net.Entidades;
using prueba_tecnica_net.Enumerables;

namespace prueba_tecnica_net.Interfaces
{
    public interface IBankService
    {
        public IEnumerable<Bank> GetAllBanks();
        public Bank GetBankById(int id);
        public Bank GetBankByName(string name);
        public IEnumerable<Bank> GetBanksByName(string name);
        public SavedStatus SaveBanks(IEnumerable<Bank> banks);
        public DeletedStatus DeleteBanks(IEnumerable<Bank> banks);
    }
}
