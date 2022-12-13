using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentService;
using RentData;
using System.Collections.Generic;
using RentDto;
using Microsoft.AspNetCore.Authorization;

namespace RentAPI2.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost, AllowAnonymous]
        //[Route("api/auth")]
        //public IActionResult Authenticate(AuthenticateRequest model)
        //{
        //    var response = _userService.Authenticate(model);

        //    if (response == null)
        //        return BadRequest(new { message = "Неверное имя пользователя или пароль" });

        //    return Ok(response);
        //}

        [HttpGet, AllowAnonymous]
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

       
        [HttpGet]
        [Route("/api/Users")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.ListUsers());
        }


        
        [HttpPost]
        [Route("/api/Users")]
        public IActionResult NewUser(User user)
        {
            return Ok(_userService.NewUser(user));
        }

        [HttpPut]
        [Route("/api/Users")]
        public IActionResult EditUser(User user)
        {
            return Ok(_userService.EditUser(user));
        }
       
        [HttpDelete]
        [Route("/api/Users/{id}")]
        public IActionResult DelUser(int id)
        {
            return Ok(_userService.DeleteUser(id));
        }
    }
}
