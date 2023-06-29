using Role_Playing_Game_API.Dtos.Fight;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.InterFaces
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto reques);    
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto reques);
    }
}
