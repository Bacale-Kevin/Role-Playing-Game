using AutoMapper;
using Role_Playing_Game_API.Dtos.Character;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
        }
    }
}
