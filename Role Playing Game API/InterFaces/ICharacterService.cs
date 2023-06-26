using Role_Playing_Game_API.Dtos.Character;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.InterFaces
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
    }
}
