using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Models;
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

        public UserController(ITranslateService translateService)
        {
            _translateService = translateService;
        }

        [HttpPost]
        public ActionResult<int> Save(User user)
        {
            return Ok(12);
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