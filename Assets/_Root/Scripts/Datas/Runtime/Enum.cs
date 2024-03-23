using System;

namespace _Root.Scripts.Datas.Runtime
{
    [Flags]
    public enum DamageType
    {
        Physical = 0,
        Fire = 1,
        Water = 2,
        Earth = 4,
        Air = 8,
        Dark = 16,
        Poison = 32,
        Plasma = Fire | Air,
        Steam = Fire | Water,
        Lava = Fire | Earth,
        Lightning = Fire | Air,
        Mud = Water | Earth,
        Ice = Water | Air,
        Nature = Air | Earth,
        Corruption = Dark | Earth,
        Darkness = Dark | Air,
        Corrosion = Dark | Water,
        Curse = Dark | Physical,
        DarkFlame = Dark | Fire,
        Acid = Water | Poison,
    }

    [Flags]
    public enum CharacterType
    {
        Enemy = 0,
        Player = 1,
        Ally = 2,
        Boss = 4,
        Summon = 8,
        Partner = 16,
        Pet = 32,
    }

    public enum FacingDirections
    {
        West = 0,
        East = 1,
        North = 2,
        South = 4
    }
}