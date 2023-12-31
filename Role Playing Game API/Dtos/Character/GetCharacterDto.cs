﻿using Role_Playing_Game_API.Dtos.SkillDto;
using Role_Playing_Game_API.Dtos.Weapon;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int MyProperty { get; set; } = 10;
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto Weapon { get; set; }
        public List<GetSkillDto> Skills { get; set; }
    }
}
