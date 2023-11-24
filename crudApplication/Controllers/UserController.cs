using crudApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace crudApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext  _dbContext;
            public UserController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            if(_dbContext.User == null)
            {
                return NotFound();
            }
            return await _dbContext.User.ToListAsync();
        }

        [HttpGet("{userId}")]
       public async Task<ActionResult<User>> GetUser(int userId)
        {
            if(_dbContext.User == null)
            {
                return NotFound();
            }
            var user = await _dbContext.User.FindAsync(userId);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>>PostUser(User user)
        {
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.userId }, user);
        }
        //[HttpPut]
        //public async Task<ActionResult> PutUser(int userId, User user)
        //{
        //    if(userId != user.userId)
        //    {
        //        return BadRequest();  
        //    }
        //    _dbContext.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    catch(DbUpdateConcurrencyException)
        //    {
        //        if(!UserAvailable(userId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return Ok();
        //}

        private bool UserAvailable(int userId)
        {
            return (_dbContext.User?.Any(x => x.userId == userId)).GetValueOrDefault();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult>DeleterUser(int userId)
        {
            if (_dbContext.User == null)
            {
                return NotFound();
            }

            var user =await _dbContext.User.FindAsync(userId);
            if(user == null)
            {
                return NotFound();
            }
            _dbContext.User.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok();

            
        }

    }
}
