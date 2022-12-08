using System.Collections.Generic;
using RentData;

namespace RentAPI2.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
        List<User> ListUsers();
        bool NewUser(User user);
        bool EditUser(User user);
        bool DeleteUser(int id);
    }
}