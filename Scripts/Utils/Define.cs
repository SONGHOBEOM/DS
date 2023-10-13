using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        ClickDown,
        ClickUp,
        Drag,
    }

    public enum ItemType
    {
        Weapon,
        Armor,
    }

    public enum Rarity
    {
        Normal,
        Rare,
        Epic,
        Heroic,
        Legendary
    }
    
    public enum WeaponType
    {
        Sword,
        BigSword
    }

    public enum PlayerClass
    {
        Adventurer,
        Warrior,
        Knight
    }

    public enum QuestType
    {
        Normal,
        Epic
    }

    public enum ObjectType
    {
        Player,
        Skeleton,
        FurySkeleton,
        BlackWizard,
        Boss
    }

    public enum WaveType
    {
        Normal,
        Boss
    }

    public enum SkillType
    {
        Active,
        Common,
    }
}
