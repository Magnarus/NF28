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
        public IntGameValue Level;

        //[CompositeGameValue]
        public FloatGameValue HP;
        //[CompositeGameValue] - exemple
        public FloatGameValue MP;

        [SimpleGameValue]
        public FloatGameValue Strength;
        [SimpleGameValue]
        public FloatGameValue Armor;
        [SimpleGameValue]
        public FloatGameValue Luck;

        public CreatureDescriptor() : base()
        {
            Level = new IntGameValue();
            HP = new FloatGameValue();
            MP = new FloatGameValue();
            Strength = new FloatGameValue();
            Armor = new FloatGameValue();
            Luck = new FloatGameValue();
        }
    }
}
