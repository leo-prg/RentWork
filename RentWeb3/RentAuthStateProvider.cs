using System.Security.Claims;

namespace RentWeb3
{
    public class RentAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //throw new NotImplementedException();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2NzA0ODI1ODgsImV4cCI6MTY3MTAwMDk4OCwiaWF0IjoxNjcwNDgyNTg4fQ.L9AN8Uct5kXewlMOyxxYId_ggEpRyabbWli9bvEmDbM";
            
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
