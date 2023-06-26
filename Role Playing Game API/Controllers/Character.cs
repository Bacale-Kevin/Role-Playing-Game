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
        public ActionResult<List<Character>> Get()
        {
            return Ok(_characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetById(int id)
        {
            return Ok(_characterService.GetCharacterById(id));

        }

        [HttpPost]
        [ProducesResponseType(201)]
        public ActionResult<List<Character>> AddCharacter(Character character)
        {
            return Ok(_characterService.AddCharacter(character));
        }
    }
}