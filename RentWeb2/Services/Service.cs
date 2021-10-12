using System.Threading.Tasks;
using Blazored.LocalStorage;
using RentWeb2.Shared;


namespace RentWeb2.Services
{

    public class Service
    {
        private ILocalStorageService _localStorage;

        public bool allowEdit { get; set; }

        public bool DevOrProd()
        {
            return ConnectAPI.apiUri.Contains("localhost");
        }

        public Service(ILocalStorageService localStorageService)
        {
            _localStorage = localStorageService;
        }

        // читаем переменную редактирования при перезапуске приложения
        public async Task Initialize()
        {
            allowEdit = await _localStorage.GetItemAsync<bool>("AllowEdit");
        }
    }
}
