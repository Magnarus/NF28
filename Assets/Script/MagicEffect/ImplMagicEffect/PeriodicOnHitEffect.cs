using UnityEngine;
using System.Collections;
using System;
using Descriptors;
using Game;

public class PeriodicOnHitEffect : MagicEffect {
    public MagicEffect effect;
    //TODO: magic effect descriptor
    public float chance = 1;

    System.Random rng = new System.Random();

    public override bool Apply(MagicEffectInstance i, GameObject to, GameObject by)
    {
        //immune
        if (!base.Apply(i, to, by))
            return false;

        //Does only works on enchantable objects
        if (!to.GetComponent<MagicEffectContainer>() || !by.GetComponent<Equipment>())
            return false;

        //TODO game events
        /*by.GetComponent<Equipment>().OnHit +=*/ i["onHit"] =  (Action<GameObject>)((x) => {
            if (x.GetComponent<MagicEffectContainer>() && rng.NextDouble() <= chance)
                x.GetComponent<MagicEffectContainer>().applyEffect(effect, this.gameObject);
        });
        return true;
    }
    public override bool Dispell(MagicEffectInstance i, GameObject from, GameObject by)
    {
        if (!base.Dispell(i, from, by))
            return false;

        //by.GetComponent<Equipment>().OnHit -= (Action)i["onHit"];
        return true;
    }
}
