﻿using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Dtos.Character
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int MyProperty { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}
