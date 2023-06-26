using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Service.InterFaces
{
    public interface ICharacterService
    {
        List<Character> GetAllCharacters();
        Character GetCharacterById(int id);
        List<Character> AddCharacter(Character newCharacter);
    }
}
