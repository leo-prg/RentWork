using RentWeb2.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Net.Http;
using RentWeb2.Shared;
using System.Net.Http.Json;
using RentCommon;


namespace RentWeb2.Services
{
    public interface IAuthenticationService 
    {
        User User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        
        public User User { get; private set; }

        

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        ) 
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItemAsync<User>("user");
        }

        public async Task Login(string username, string password)
        {
            //password = BCrypt.Net.BCrypt.HashPassword(password);//PwdCrypt.Encrypt(password);
            try
            {
                User = await _httpService.Post<User>(ConnectAPI.apiUri + "/api/auth", new { username, password });
            }
            catch (System.Exception)
            {
                throw ;
            }
            

            await _localStorageService.SetItemAsync("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItemAsync("user");
            _navigationManager.NavigateTo("login");
        }

    }
}