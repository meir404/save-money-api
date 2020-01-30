using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Models;
using Logic.Repositories;
using Logic.Services.Interfases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace save_money_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UserController : ControllerBase
    {
        private readonly ITranslateService _translateService;
        private readonly UserRepository _userRepository;

        public UserController(ITranslateService translateService, UserRepository userRepository)
        {
            _translateService = translateService;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Save(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(await _userRepository.Save(user));
        }

        [HttpGet, Route("current")]
        public ActionResult<User> GetCurrentUser()
        {
            return Ok(new User { });
        }

        [HttpGet]
        public ActionResult<User> Get(int id)
        {
            return Ok(new User
            {
                Email = _translateService.Translate("meir.com")

            });
        }

        [HttpPut]
        public ActionResult<int> Update(User user)
        {
            return Ok(12);
        }

        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
            return Ok(true);
        }
    }
}