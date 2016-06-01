using UnityEngine;
using System.Collections;
using Descriptors;
using Game;
using System.Collections.Generic;
using System.Linq;

//Can receive after-market modifications (player enchantment = poisoned dagger)
[RequireComponent(typeof(MagicEffectContainer))]
//Descriptor
[RequireComponent(typeof(EquipmentDescriptor))]
public class Equipment : MonoBehaviour {
    [BitMask(typeof(EquipmentTag))]
    public EquipmentTag Description;

    public void ReEquip(EquipmentInstance i, GameObject target)
    {
        Unequip(i, target);
        Equip(i, target);
    }

    public void Equip(EquipmentInstance i, GameObject target)
    {
        var descriptor = gameObject.GetComponent<EquipmentDescriptor>();
        var toMod = target.GetComponent<CreatureDescriptor>();
        var effectList = target.GetComponent<MagicEffectContainer>();
        var equipmentContainer = target.GetComponent<EquipmentContainer>();

        equipmentContainer.Unequip(Description & EquipmentTag.SLOT);


        i["statsModifiers"] = descriptor.Join(toMod,
            x => x.Key,
            y => y.Key,
            (x, y) => x.Value.ApplyEffect(z => z + y.Value.value)
        );

        //i["Enchant"] = effectList.applyEffect(Enchantment, this.gameObject);
    }

    public void Unequip(EquipmentInstance i, GameObject target)
    {
        var effectList = target.GetComponent<MagicEffectContainer>();
        foreach (var item in i["statsModifiers"] as IEnumerable<ModifierInstance<float>>)
        {
            item.Dispose();
        }

        //effectList.dispellEffect((MagicEffectInstance)i["Enchant"]);
    }
}
