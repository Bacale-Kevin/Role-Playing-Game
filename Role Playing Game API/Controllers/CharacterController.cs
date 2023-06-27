using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Role_Playing_Game_API.Data;
using Role_Playing_Game_API.Dtos.Character;
using Role_Playing_Game_API.InterFaces;
using Role_Playing_Game_API.Models;
using Role_Playing_Game_API.Service;

namespace Role_Playing_Game_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id)
        {
            var response = await _characterService.GetCharacterById(id);
            if(response.Data == null)
                return NotFound(response);

            return Ok(await _characterService.GetCharacterById(id));

        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacter(UpdateCharacterDto updateCharacter)
        {
            var response = await _characterService.UpdateCharacter(updateCharacter);
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);

        }
    }
}