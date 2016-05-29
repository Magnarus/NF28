using UnityEngine;
using System.Collections;
using System;
using Descriptors;
using Game;

public class StandardDotEffect : MagicEffect {
    public float magnitude;

    public override bool Apply(MagicEffectInstance i, GameObject to, GameObject by)
    {
        //immune
        if (!base.Apply(i, to, by))
            return false;

        //Dot only works on creature
        if (!to.GetComponent<CreatureDescriptor>())
            return false;

        //TODO resistance
        i["damage"] = magnitude;

        //TODO game events
        /*Game.Events.OnTurnStart +=*/ i["onTurnStart"] =  (Action)(() => {
                                           to.GetComponent<CreatureDescriptor>().HP.Damage((float)i["damage"]);
        });
        return true;
    }
    public override bool Dispell(MagicEffectInstance i, GameObject from, GameObject by)
    {
        if (!base.Dispell(i, from, by))
            return false;

        //Game.Events.OnTurnStart -= (Action)i["onTurnStart"];
        return true;
    }
}
