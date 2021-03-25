using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kartaca.API.Models;

namespace Kartaca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// UserId ye sahip kullanıcıyı getirir
        /// </summary>
        /// <param name="userId">Kullanıcının id'si</param>
        /// <response code="200">Kullanıcıyı döner</response>
        /// <response code="404">Eğer kullanıcı bulunamazsa</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        /// <summary>
        /// Tüm kullanıcıları getirir
        /// </summary> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }
        /// <summary>
        /// Bir kullanıcı oluşturur
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///        "firstName": "Ali",
        ///        "lastName": "Yıldızöz"
        ///     }
        ///
        /// </remarks>
        /// <param name="user"></param>
        /// <response code="201">Oluşturulan kullanıcıyı id'si ile döner</response>
        /// <response code="400">Eğer kullanıcı null ise</response>            
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Post(User user = null)
        {
            if (ModelState.IsValid)
            {
                _userService.Add(user);
                return Created("", user);
            }

            return BadRequest();
        }
        /// <summary>
        /// Bir kullanıcının bilgilerini günceller
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///        "id": "..."
        ///        "firstName": "Ali",
        ///        "lastName": "Yıldızöz"
        ///     }
        ///
        /// </remarks>
        /// <param name="user"></param>
        /// <param name="userId">Kullanıcının id'si</param>
        /// <response code="200">Güncelleme başarılı ise</response>
        /// <response code="400">Eğer kullanıcı veya id null ise</response>            
        /// <response code="404">Eğer kullanıcı bulunamazsa</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{userId}")]
        public IActionResult Put(int? userId = null, User user = null)
        {
            if (ModelState.IsValid)
            {
                var res = _userService.GetById(userId.Value);
                if (res == null)
                {
                    return NotFound();
                }

                user.Id = userId.Value;
                _userService.Update(user);
                return Ok();
            }

            return BadRequest();
        }
        /// <summary>
        /// Bir kullanıcıyı verilen id'ye göre siler
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="200">Silme işlemi başarılı ise</response>           
        /// <response code="404">Eğer kullanıcı bulunamazsa</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            _userService.Remove(user);
            return Ok();
        }


    }
}
