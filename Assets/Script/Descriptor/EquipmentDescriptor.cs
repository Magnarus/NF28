using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Descriptors
{
    public class EquipmentDescriptor : Descriptor
    {
        [SimpleGameValue]
        public FloatGameValue HP;
        [SimpleGameValue]
        public FloatGameValue MP;

        [SimpleGameValue]
        public FloatGameValue Strength;
        [SimpleGameValue]
        public FloatGameValue Armor;
        [SimpleGameValue]
        public FloatGameValue Luck;

        public EquipmentDescriptor() : base()
        {
            HP = new FloatGameValue();
            MP = new FloatGameValue();
            Strength = new FloatGameValue();
            Armor = new FloatGameValue();
            Luck = new FloatGameValue();
        }
    }
}
