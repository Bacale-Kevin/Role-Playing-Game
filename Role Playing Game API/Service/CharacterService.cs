using AutoMapper;
using Role_Playing_Game_API.Dtos.Character;
using Role_Playing_Game_API.InterFaces;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Service
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;

        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(character => character.Id) + 1;
            characters.Add(character);

            response.Data = characters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList();

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = characters
                .Select(character => _mapper.Map<GetCharacterDto>(character)).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(character => character.Id == id);
            response.Data = _mapper.Map<GetCharacterDto>(character);

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = characters.FirstOrDefault(character => character.Id == updateCharacter.Id);

                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Class = updateCharacter.Class;

                response.Data = _mapper.Map<GetCharacterDto>(character);


            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;

            }

            return response;
        }
    }
}
