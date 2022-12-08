using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentAPI2.Services;
using RentData;
using System.Collections.Generic;
using RentDto;
using Microsoft.AspNetCore.Authorization;

namespace RentAPI2.Controllers
{
    [ApiController]
    [EnableCors]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/auth")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Неверное имя пользователя или пароль" });

            return Ok(response);
        }

        [HttpGet]
        [Route("/api/Users/List")]
        public IActionResult ListUsers()
        {
            List<UserList> ul = new List<UserList>();
            foreach (User u in _userService.ListUsers())
            {
                ul.Add(new UserList() { Name = string.Format("{0} {1}", u.LastName, u.FirstName), UserName = u.Username });
            }
            return Ok(ul);
        }

        [Authorize]
        [HttpGet]
        [Route("/api/Users")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.ListUsers());
        }


        [Authorize]
        [HttpPost]
        [Route("/api/Users")]
        public IActionResult NewUser(User user)
        {
            return Ok(_userService.NewUser(user));
        }

        [Authorize]
        [HttpPut]
        [Route("/api/Users")]
        public IActionResult EditUser(User user)
        {
            return Ok(_userService.EditUser(user));
        }

        [Authorize]
        [HttpDelete]
        [Route("/api/Users/{id}")]
        public IActionResult DelUser(int id)
        {
            return Ok(_userService.DeleteUser(id));
        }
    }
}
