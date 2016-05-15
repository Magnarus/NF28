using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game
{
    //The game value has no current value
    //Utilisé par l'editor pour savoir qu'il n'a besoin d'afficher qu'un seul champ
    //Quand on prog on a juste à le laisser à null, c'est vraiment que pour l'editor
    [System.AttributeUsage(System.AttributeTargets.Field |
                       System.AttributeTargets.Property,
                       AllowMultiple = false)]
    public class SimpleGameValueAttribute : Attribute
    {

    }

    [Serializable]
    public class GameValue<T>
        where T: IComparable
    {
        //Actual game value (saved)
        [SerializeField]
        T MaxValue = default(T);
        [SerializeField]
        T _CurrentValue;
        public T CurrentValue {
            get
            {
                return _CurrentValue;
            }
            internal set
            {
                _CurrentValue = value;
                _CurrentValue = _CurrentValue.CompareTo(MaxValue) > 0 ? MaxValue : CurrentValue;
            }
        }

        //modifier handler
        uint currentBuffId = 0;
        SortedDictionary<uint, Func<T, T>> modifiers = new SortedDictionary<uint, Func<T, T>>();

        //MaxValue: modifié par les modifiers directement
        //CurrentValue: modifié uniquement si elle dépasse MaxValue après traitement
        //Exemple: buff de HP = buff de HP max mais n'augmente pas les points de vie actuel sauf si le buff utilise un restore dans sa routine
        //Exemple: debuff de HP sur cible full life = HP réduit au nouveau max donc automatiquement des dégats mais aucun dommage sur quelqu'un à qui il reste un HP (juste le debuff)
        T calcModifiedGameValue()
        {
            T unmodifiedValue = MaxValue;
            foreach (var pair in modifiers)
            {
                unmodifiedValue = pair.Value(unmodifiedValue);
            }

            //compare current à max. Si le nouveau max < current, current est réduit aussi (IComparable bitches!) 
            if (CurrentValue != null)
                CurrentValue = CurrentValue.CompareTo(MaxValue) > 0 ? MaxValue : CurrentValue;

            return unmodifiedValue;
        }

        public T value
        {
            get
            {
                return calcModifiedGameValue();
            }
            set
            {
                if (CurrentValue != null)
                    CurrentValue = CurrentValue.CompareTo(MaxValue) > 0 ? MaxValue : CurrentValue;
                MaxValue = value;
            }
        }

        public T BaseValue
        {
            get
            {
                return MaxValue;
            }
            set
            {
                this.value = value;
            }
        }



        public uint ApplyModifier(Func<T,T> f)
        {
            modifiers.Add(++currentBuffId, f);
            calcModifiedGameValue();
            return currentBuffId;
        }

        public void RemoveModifier(uint id)
        {
            modifiers.Remove(id);
            calcModifiedGameValue();
        }
    }

    static class GameValueSpecialization
    {
        public static void Damage(this GameValue<float> v, float amount)
        {
            v.CurrentValue = Math.Max(v.CurrentValue-amount, 0);
        }
        public static void Restore(this GameValue<float> v, float amount)
        {
            v.CurrentValue = Math.Min(v.CurrentValue+amount, v.value);
        }
    }


}
