﻿using UnityEngine;
using System.Collections;
using System;
using Descriptors;
using Game;

public class PeriodicTriggerEffect : MagicEffect {
    public MagicEffect effect;
    public float chance = 1;


    System.Random rng = new System.Random();
    public override bool Apply(MagicEffectInstance i, GameObject to, GameObject by)
    {
        //immune
        if (!base.Apply(i, to, by))
            return false;

        //Dot only works on enchentable objects
        if (!to.GetComponent<MagicEffectContainer>())
            return false;

        //TODO game events
        /*Game.Events.OnTurnStart +=*/ i["onTurnStart"] =  (Action)(() => {
            if (rng.NextDouble() <= chance)
                to.GetComponent<MagicEffectContainer>().applyEffect(effect, this.gameObject);
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
