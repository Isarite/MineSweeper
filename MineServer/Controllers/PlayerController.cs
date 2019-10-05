/**
 * @(#) PlayerController.cs
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MineServer.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
//TODO player handling
//TODO context class
//TODO REST request handling
//TODO edit model
namespace MineServer.Controllers
{
    [Route("api/player")]
    public class PlayerController : ControllerBase
	{
        private readonly SignInManager<Player> _signManager;
        private readonly UserManager<Player> _userManager;
        private readonly MineSweeperContext _context;

        public PlayerController(MineSweeperContext context, UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            context.Database.EnsureCreated();
            _context = context;
            _userManager = userManager;
            _signManager = signInManager;

        }

        Player players;
		
		int playerCount;
		
		PlayerController instance;
		
		ICommand games;

        //public void StartGame(  )
        //{

        //}


        //public void EndGame(  )
        //{

        //}

        //public void DoAction(  )
        //{

        //}

        //public void GetInstance(  )
        //{
        //          _context.Users.Add(new Player { UserName = test, pass})
        //}


        //      public PlayerController(PlayerContext context)
        //      {
        //          _context = context;

        //          if (_context.Players.Count() == 0)
        //          {
        //              // Create a new Player if collection is empty,
        //              // which means you can't delete all Players.
        //              for (int i = 0; i < 10; i++)
        //              {
        //                  Qty++;
        //                  Player p = new Player { Name = "Player-" + Qty, Score = 0, PosX = 0, PosY = 0 };
        //                  _context.Players.Add(p);
        //              }

        //              _context.SaveChanges();
        //          }
        //      }


        //      // GET api/player
        //      [HttpGet]
        //      public ActionResult<IEnumerable<Player>> GetAll()
        //      {
        //          return _context.Players.ToList();
        //      }

        //      private void CreateUser(string name, string password)
        //      {

        //      }

        //      // GET api/player/5
        //      [HttpGet("{id}", Name = "GetPlayer")]
        //      public ActionResult<Player> GetById(long id)
        //      {
        //          Player p = _context.Players.Find(id);
        //          if (p == null)
        //          {
        //              return NotFound("player not found");
        //          }
        //          return p;
        //      }

        // POST api/player
        [HttpPost]
        //public string Create(Player player)
        public async Task<IActionResult> Create([FromBody] PlayerData player)
        {

            //return Ok(); //"created - ok"; 
            string name = player.userName;
            string pass = player.password;

            await _userManager.CreateAsync(new Player { UserName = name });
            var user = await _userManager.FindByNameAsync(name);
            var res = await _userManager.AddPasswordAsync(user, pass);

            String hashedNewPassword = _userManager.PasswordHasher.HashPassword(user,pass);
            UserStore<Player> store = new UserStore<Player>(_context);
            await store.SetPasswordHashAsync(user, hashedNewPassword);


            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetToken([FromBody] PlayerData player)
        {
            var user = await _userManager.FindByNameAsync(player.userName);
            var result = await _signManager.PasswordSignInAsync(user, player.password, false, false);
            byte[] token;
            if (result.Succeeded)
            {
                token = await _userManager.CreateSecurityTokenAsync(user);
                await _userManager.SetAuthenticationTokenAsync(user,TokenOptions.DefaultProvider,"token", token.ToString());
                return Ok(token);
            }
            return UnprocessableEntity();
        }

        [Route("[action]")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ConfirmToken()
        {
            var accessToken = Request.Headers["Authorization"];
            string userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return UnprocessableEntity(await _userManager.FindByIdAsync(userId));
        }

        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DoAction()//[FromBody] Action action
        {
            var accessToken = Request.Headers["Authorization"];
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Player player = await _userManager.FindByIdAsync(userId);
            //actions
            return Ok();
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public IActionResult Update(long id, [FromBody] Player p)
        //{
        //    var pp = _context.Players.Find(id);
        //    if (pp == null)
        //    {
        //        return NotFound();
        //    }

        //    pp.Name = p.Name;
        //    pp.PosX = p.PosX;
        //    pp.PosY = p.PosY;
        //    pp.Score = p.Score;

        //    _context.Players.Update(pp);
        //    _context.SaveChanges();

        //    return Ok(); //NoContent();
        //}

        //[HttpPatch]
        //public IActionResult PartialUpdate([FromBody] Coordinates request)
        //{
        //    var player = _context.Players.Find(request.Id);
        //    if (player == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        player.PosX = request.PosX;
        //        player.PosY = request.PosY;

        //        _context.Players.Update(player);
        //        _context.SaveChanges();
        //    }
        //    return Ok();
        //    //return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
        //}

        //// DELETE api/values/5
        ///*[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}*/
        //[HttpDelete("{id}")]
        //public IActionResult Delete(long id)
        //{
        //    var todo = _context.Players.Find(id);
        //    if (todo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Players.Remove(todo);
        //    _context.SaveChanges();
        //    return NoContent();
        //}
    }

}
