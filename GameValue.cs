using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    
    struct GameValue<T> where T: IComparable
    {
        uint currentBuffId;

        T MaxValue;
        T CurrentValue;

        SortedDictionary<uint, Func<T, T>> modifiers;

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
            CurrentValue = CurrentValue.CompareTo(MaxValue) > 0 ? MaxValue : CurrentValue;

            return unmodifiedValue;
        }

        public T value
        {
            get
            {
                return calcModifiedGameValue();
            }
        }

        public uint ApplyModifier(Func<T,T> f)
        {
            if (modifiers == null)
                modifiers = new SortedDictionary<uint, Func<T, T>>();

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
}
