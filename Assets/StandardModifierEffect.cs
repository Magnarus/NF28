using UnityEngine;
using System.Collections;
using System;
using Descriptors;
using Game;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(EquipmentDescriptor))]
public class StandardModifierEffect : MagicEffect {

    public override bool Apply(MagicEffectInstance i, GameObject to, GameObject by)
    {
        //immune
        if (!base.Apply(i, to, by))
            return false;

        //Dot only works on items
        if (!to.GetComponent<EquipmentDescriptor>())
            return false;

        var descriptor = this.gameObject.GetComponent<EquipmentDescriptor>();
        var toMod = to.gameObject.GetComponent<EquipmentDescriptor>();

        i["statsModifiers"] = descriptor.Join(toMod, 
            x => x.Key, 
            y => y.Key, 
            (x, y) => x.Value.ApplyEffect(z => z + y.Value.value)
        );
        return true;
    }
    public override bool Dispell(MagicEffectInstance i, GameObject from, GameObject by)
    {
        if (!base.Dispell(i, from, by))
            return false;

        foreach (var item in i["statsModifiers"] as IEnumerable<ModifierInstance<float>>)
        {
            item.Dispose();
        }

        //Game.Events.OnTurnStart -= (Action)i["onTurnStart"];
        return true;
    }
}
