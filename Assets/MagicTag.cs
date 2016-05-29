using System;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field |
                       System.AttributeTargets.Property,
                       AllowMultiple = false)]
public class BitMaskAttribute : PropertyAttribute
{
    public System.Type propType;
    public BitMaskAttribute(System.Type aType)
    {
        propType = aType;
    }
}
[Flags]
public enum MagicTag
{
    //elemental
    SCHOOL_PHYSICAL = 1 << 0,
    SCHOOL_FIRE = 1 << 1,
    SCHOOL_WATER = 1 << 2,
    SCHOOL_AIR = 1 << 3,
    SCHOOL_EARTH = 1 << 4,

    //nature
    NATURE_BENEFICIAL = 1 << 5,

    //mechanic
    MECHANIC_DISABLE = 1 << 6,
    MECHANIC_ROOT = 1 << 7,
    MECHANIC_SNARE = 1 << 8,

    //type
    TYPE_CURSE = 1 << 9,
    TYPE_BLEED = 1 << 11,
    TYPE_ENRAGE = 1 << 12,
    TYPE_POISON = 1 << 13,
    TYPE_DESEASE = 1 << 14,

}