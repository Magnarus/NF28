using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{

    class Modifier<T, K> 
        where K: IDisposable 
        where T : IComparable
    {
        K parent;
        Func<T, T> effect;
        public Modifier(K myParent, Func<T, T> myEffect)
        {
            parent = myParent;
            effect = myEffect;
        }

        public void ApplyTo(GameValue<T> v)
        {
            v.ApplyModifier(effect);
        }
    }
}
