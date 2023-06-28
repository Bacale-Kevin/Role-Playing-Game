using Role_Playing_Game_API.Dtos.Character;
using Role_Playing_Game_API.Dtos.Weapon;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.InterFaces
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon );
    }
}
