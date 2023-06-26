using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
       

        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetById(int id)
        {

            var character = characters.FirstOrDefault(character => character.Id == id);

            if(character == null) 
                return NotFound(id.ToString());

            return Ok(character);
        }

        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character character)
        {
            characters.Add(character);
            
            return Ok(characters);
        }
    }
}