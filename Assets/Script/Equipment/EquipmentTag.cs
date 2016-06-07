using System;
using UnityEngine;

[Flags]
public enum EquipmentTag
{
    //slot
    SLOT_ARMOR = 1 << 0,
    SLOT_WEAPON = 1 << 1,
    SLOT_SHIELD = 1 << 2,

    SLOT = SLOT_WEAPON | SLOT_SHIELD | SLOT_ARMOR,

    //armor type
    TYPE_HEAVY = 1 << 3,
    TYPE_CLOTH = 1 << 4,

    //weapon type
    TYPE_SWORD = 1 << 5,
    TYPE_BOW = 1 << 6,
    TYPE_TOME = 1 << 7
}