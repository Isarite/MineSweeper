/**
 * @(#) PlayerController.cs
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineServer.Models;
using MineServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
//TODO Game Controller
namespace MineServer.Controllers
{
    [Route("api/player")]
    public class PlayerController : ControllerBase
	{
        private readonly GameFacade _facade;

        public PlayerController(GameFacade facade)
        {
            _facade = facade;
        }


        // POST api/player
        [HttpPost]
        //public string Create(Player player)
        public async Task<IActionResult> Create([FromBody] PlayerData player)
        {
            if (player == null || player.userName == null || player.password == null)
                return BadRequest();
            string name = player.userName;
            string pass = player.password;
            var result =  await _facade.CreatePlayer(name, pass);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> GetToken([FromBody] PlayerData player)
        {
            var token = await _facade.GetToken(player);
            if (token != null)
                return Ok(token);
            return NotFound();
        }

        [Route("[action]/{id}")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DoMove([FromBody] Move move, int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _facade.DoMove(move, id, userId);
            if (result != null)
            {
                if (!result.success)
                    return BadRequest();
                return Ok(result);
            }
            return Unauthorized();
        }

        [Route("[action]/{id}")]
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Surrender(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _facade.Surrender(id, userId);
            if (result != null)
            {
                if (!result.success)
                    return BadRequest();
                return Ok(result);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Starts or joins game for player
        /// </summary>
        /// <returns>Started game id</returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> StartGame()//[FromBody] Move move
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _facade.StartGame(userId);

            if (result != null)
                return Ok(result);

            return BadRequest();
        }
        
        // Get api/Update/values/5
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _facade.Update(userId, id);

            if (result != null)
            {
                if (!result.success)
                    return BadRequest();
                return Ok(result);
            }

            return Unauthorized();
        }
    }

}
