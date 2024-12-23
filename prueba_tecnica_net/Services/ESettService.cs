using Newtonsoft.Json;
using prueba_tecnica_net.Entidades;
using prueba_tecnica_net.Interfaces;

namespace prueba_tecnica_net.Services
{
    public class ESettService : IESettService
    {
        private readonly string _url = "https://api.opendata.esett.com/EXP06/Banks";

        public List<Bank> GetBanksData()
        {
            var httpClient = new HttpClient();

            var result = httpClient.GetAsync(_url).Result;

            if (result.StatusCode is not System.Net.HttpStatusCode.OK)
                return new List<Bank>();

            string body = result.Content.ReadAsStringAsync().Result;
            List<Bank> banks = JsonConvert.DeserializeObject<List<Bank>>(body);

            return banks ?? new List<Bank>();
        }
    }
}
