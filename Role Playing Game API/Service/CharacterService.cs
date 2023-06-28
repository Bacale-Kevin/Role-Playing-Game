using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Role_Playing_Game_API.Data;
using Role_Playing_Game_API.Dtos.Character;
using Role_Playing_Game_API.InterFaces;
using Role_Playing_Game_API.Models;
using System.Security.Claims;

namespace Role_Playing_Game_API.Service
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);

            //Get authenticated user Id and assign it to the User field of the Character entity
            character.User = await _context.Users.FirstOrDefaultAsync(user => user.Id == GetUserId());

            _context.Characters.Add(character); // Tracking data to be added to DB
            await _context.SaveChangesAsync(); // Insert changes to the DB

            response.Data = await _context.Characters
                .Where(character => character.User.Id == GetUserId())
                .Select(character => _mapper.Map<GetCharacterDto>(character))
                .ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            //Only get the Character created by the authenticated user
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters
                .Where(character => character.User.Id == GetUserId())
                .ToListAsync();

            response.Data = dbCharacters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacter = await _context.Characters
                    .FirstOrDefaultAsync(character => character.Id == id && character.User.Id == GetUserId()); // Only get or see the character created by user
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
                    .Include(character => character.User) // To access related entities we need to include them first
                    .FirstOrDefaultAsync(character => character.Id == updateCharacter.Id);

                if (character.User.Id == GetUserId())
                {

                    character.Name = updateCharacter.Name;
                    character.HitPoints = updateCharacter.HitPoints;
                    character.Strength = updateCharacter.Strength;
                    character.Defense = updateCharacter.Defense;
                    character.Class = updateCharacter.Class;

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character not found";

                    return response;
                }



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
                // (FirstOrDefault) return null if no elements was found while (First) does not return null but throws an exception
                var character = await _context.Characters
                    .FirstOrDefaultAsync(character => character.Id == id && character.User.Id == GetUserId());

                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();

                    response.Data = await _context.Characters
                        .Where(character => character.User.Id == GetUserId())
                        .Select(character => _mapper.Map<GetCharacterDto>(character)).ToListAsync();
                }
                else
                {

                    response.Success = false;
                    response.Message = "Character not found";

                    return response;

                }



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
