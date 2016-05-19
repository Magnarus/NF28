using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    //USAGE:
    //BuffForce = Joueur.Force.ApplyEffect(Modifier.Float.Add(5))
    //BuffForce.Dispose();
    //Pourquoi séparer de Gamevalue? Résilience, si on perd la référence d'un buff dans le code, le buff s'estompe tout seul au prochain cycle de GC. Je pense au déboires du système d'AV de TES IV et V
    //Ceci rend tout exploit de type buff stacking mécaniquement impossibe ou tout du moins temporaire et non persistant
    //note: tout les buff ne concernent pas les stats, la classes buff sera une aggreg' de ModifierInstance et d'autre choses 
    //(script effect par exemple, ou pour un poison, un truc qui applique des modifiers tout les tours)
    //une armure sera une aggreg de ModifierInstance aussi pour les stats que ça donne (armure, HP, chance, ...) avec peut-être un buff dessus pour l'enchantement (life steal, mirroir physique)

    class ModifierInstance<T> : IDisposable
        where T : IComparable
    {
        Func<T, T> effect;
        uint modId = 0;
        GameValue<T> moddedValue;

        internal ModifierInstance(Func<T, T> myEffect)
        {
            effect = myEffect;
        }
        ~ModifierInstance()
        {
            if (modId != 0)
                Dispose();
        }

        public void ApplyTo(GameValue<T> v)
        {
            moddedValue = v;
            modId = v.ApplyModifier(effect);
        }

        public void Dispose()
        {
            moddedValue.RemoveModifier(modId);
        }
    }

    static class ModifierFactory
    {
        public static ModifierInstance<T> ApplyEffect<T>(this GameValue<T> v, Func<T, T> myEffect)
            where T: IComparable
        {
            var ret = new ModifierInstance<T>(myEffect);
            ret.ApplyTo(v);
            return ret;
        }
    }

    //Chainable modifier generator
    //Exemple: Modifier.Float.Add(5).Ceil(10) est un effet qui buff une stat de 5 mais ce buff ne peut pas faire dépasser 10
    //Exemple: Modifier.Float.Multiply(2).Ceil(Modifier.Float.add(150)) impossible, mais bon on risque pas d'en avoir besoin
    static class Modifier
    {
        //Begin float chain
        public static readonly Func<float, float> Float = x => x;

        //float chain
        public static Func<float, float> Add(this Func<float, float> r, float value) { return x => r(x) + value; }
        public static Func<float, float> Multiply(this Func<float, float> r, float value) { return x => r(x) * value; }
        public static Func<float, float> Ceil(this Func<float, float> r, float value) { return x => Math.Min(r(x), value); }
        public static Func<float, float> Floor(this Func<float, float> r, float value) { return x => Math.Max(r(x), value); }
    }
}
