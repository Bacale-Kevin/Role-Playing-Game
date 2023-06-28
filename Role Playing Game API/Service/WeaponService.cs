using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Role_Playing_Game_API.Data;
using Role_Playing_Game_API.Dtos.Character;
using Role_Playing_Game_API.Dtos.Weapon;
using Role_Playing_Game_API.InterFaces;
using Role_Playing_Game_API.Models;
using System.Security.Claims;

namespace Role_Playing_Game_API.Service
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeaponService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try
            {
                //Get the Character linked to the Weapon
                var character = await _context.Characters
                    .FirstOrDefaultAsync(character => character.Id == newWeapon.CharacterId &&
                    character.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";

                    return response;
                }

                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character,
                };

                _context.Add(weapon);
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
    }
}
