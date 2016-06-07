using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game;
using UnityEditor;
using UnityEngine;

namespace Descriptors
{
    [Serializable]
    public class CreatureDescriptor : Descriptor
    {
        [SimpleGameValue]
        public GameValue<int> Level;

        //[CompositeGameValue]
        public GameValue<float> HP;
        //[CompositeGameValue] - exemple
        public GameValue<float> MP;

        [SimpleGameValue]
        public GameValue<float> Strength;
        [SimpleGameValue]
        public GameValue<float> Armor;
        [SimpleGameValue]
        public GameValue<float> Luck;

        public CreatureDescriptor() : base()
        {
            Level = new GameValue<int>();
            HP = new GameValue<float>();
            MP = new GameValue<float>();
            Strength = new GameValue<float>();
            Armor = new GameValue<float>();
            Luck = new GameValue<float>();
        }
    }
}
