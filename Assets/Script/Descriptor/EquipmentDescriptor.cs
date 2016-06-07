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
        public GameValue<float> HP;
        [SimpleGameValue]
        public GameValue<float> MP;

        [SimpleGameValue]
        public GameValue<float> Strength;
        [SimpleGameValue]
        public GameValue<float> Armor;
        [SimpleGameValue]
        public GameValue<float> Luck;

        public EquipmentDescriptor() : base()
        {
            HP = new GameValue<float>();
            MP = new GameValue<float>();
            Strength = new GameValue<float>();
            Armor = new GameValue<float>();
            Luck = new GameValue<float>();
        }
    }
}
