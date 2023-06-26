using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Role_Playing_Game_API.Models;
using Role_Playing_Game_API.Service;

namespace Role_Playing_Game_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly CharacterService _characterService;

        public CharacterController(CharacterService characterService)
        {
            _characterService = characterService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Character>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetById(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));

        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<List<Character>>> AddCharacter(Character character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }
    }
}