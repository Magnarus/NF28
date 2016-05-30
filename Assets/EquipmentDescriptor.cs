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
    }
}
