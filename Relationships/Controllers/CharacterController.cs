using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relationships.Data;
using Relationships.Models;

namespace Relationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet(Name = nameof(GetAllCharacters))]
        public async Task<ActionResult<IEnumerable<Character>>> GetAllCharacters()
        {
            if (_context.Characters is null)
            {
                return NotFound();
            }
            return Ok(await _context.Characters.Include(i => i.User).ToListAsync());
        }

        [HttpGet("userId={userId:int}", Name = nameof(GetCharactersByUserId))]
        public async Task<ActionResult<Character>> GetCharactersByUserId(int userId)
        {
            var characters = _context.Characters
                .Include(i => i.User)
                .Where(c => c.UserId == userId);
            if (characters is null || !await characters.AnyAsync())
            {
                return NotFound();
            }
            return Ok(await characters.ToListAsync());
        }
    }
}
