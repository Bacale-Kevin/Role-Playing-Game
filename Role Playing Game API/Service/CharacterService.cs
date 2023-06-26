using Role_Playing_Game_API.InterFaces;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Service
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var response = new ServiceResponse<List<Character>>();
            characters.Add(newCharacter);

            response.Data = characters;

            return response;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            return new ServiceResponse<List<Character>> { Data = characters }; 
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<Character>();
            var character = characters.FirstOrDefault(character => character.Id == id);
            response.Data = character;

            return response;
        }
    }
}
