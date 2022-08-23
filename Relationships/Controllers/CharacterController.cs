using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relationships.Data;
using Relationships.Models;
using Relationships.Models.DTO;

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
        public async Task<ActionResult<List<CharacterDTO>>> GetAllCharacters()
        {
            if (_context.Characters is null)
            {
                return NotFound();
            }

            var charactersFromDb = _context.Characters.Include(i => i.User);
            var charactersDTO = new List<CharacterDTO>();
            foreach (var character in charactersFromDb)
            {
                charactersDTO.Add(new CharacterDTO
                {
                    Id = character.Id,
                    Name = character.Name,
                    Class = character.Class,
                    Username = character.User.Username
                });
                await Task.CompletedTask;
            }

            return Ok(charactersDTO);
        }

        [HttpGet("userId={userId:int}", Name = nameof(GetCharactersByUserId))]
        public async Task<ActionResult<List<CharacterDTO>>> GetCharactersByUserId(int userId)
        {
            var charactersFromDb = _context.Characters
                .Include(i => i.User)
                .Where(c => c.UserId == userId);
            if (charactersFromDb is null || !await charactersFromDb.AnyAsync())
            {
                return NotFound();
            }

            var charactersDTO = new List<CharacterDTO>();
            foreach (var character in charactersFromDb)
            {
                charactersDTO.Add(new CharacterDTO
                {
                    Id = character.Id,
                    Name = character.Name,
                    Class = character.Class,
                    Username = character.User.Username
                });
            }

            return Ok(charactersDTO);
        }
    }
}
