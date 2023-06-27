using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Role_Playing_Game_API.Data;
using Role_Playing_Game_API.Dtos.Character;
using Role_Playing_Game_API.InterFaces;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Service
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);

            _context.Characters.Add(character); // Tracking data to be added to DB
            await _context.SaveChangesAsync(); // Insert changes to the DB

            response.Data = await _context.Characters
                .Select(character => _mapper.Map<GetCharacterDto>(character))
                .ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId) 
        {
            //Only get the Character created by the authenticated user
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters
                .Where(character => character.User.Id == userId)
                .ToListAsync();

            response.Data = dbCharacters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(character => character.Id == id);
                response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);

            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = "Not Found";
            }
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(character => character.Id == updateCharacter.Id);

                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Class = updateCharacter.Class;

                await _context.SaveChangesAsync();
                 
                response.Data = _mapper.Map<GetCharacterDto>(character);


            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;

            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                // FirstOrDefault return null if no elements was found while First throws an exception
                var character = await _context.Characters
                    .FirstAsync(character => character.Id == id);

                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

                response.Data = await _context.Characters
                    .Select(character => _mapper.Map<GetCharacterDto>(character)).ToListAsync();

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
