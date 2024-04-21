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
        Enemy = 1<<0,
        Player = 1<<1,
        Boss = 1<<3,
        Summon = 1<<4,
        Pet = 1<<5,
        Npc = 1<<6,
        UniqueNpc = 1<<7,
        Male = 1<<8,
        Female = 1<<9,
    }

    public enum FacingDirections
    {
        West = 0,
        East = 1,
        North = 2,
        South = 4
    }
    
    
    public enum ActivityState { Queued, Active, Canceling, Done }
}