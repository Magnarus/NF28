using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EquipmentContainer : MonoBehaviour {
    const int recursiveComplexityLimit = 5;


    IEnumerable<EquipmentInstance> Equipment = new List<EquipmentInstance>();
    int recursiveComplexity = 0;

    void addComplexity()
    {
        if (++recursiveComplexity > recursiveComplexityLimit)
        {
            Equipment = Equipment.ToArray();
            recursiveComplexity = 0;
        }

    }

    public void Equip(Equipment equipment)
    {
        EquipmentInstance i = new EquipmentInstance();
        i.target = this.gameObject;

        //Act on a copy
        i.baseItem = GameObject.Instantiate(equipment.gameObject).GetComponent<Equipment>();
        equipment.Equip(i, this.gameObject);
        Equipment.Push(i);
    }

    public void Unequip(EquipmentTag tags)
    {
        var removable = Equipment
            .Where(x => (tags & x.baseItem.Description) != 0);

        var removed = removable.ToArray();

        if (removed.Count() <= 0)
            return;

        foreach (var item in removed)
        {
            GameObject.Destroy(item.baseItem.gameObject);
            item.Unequip();
        }


        Equipment = Equipment.Except(removed);
        addComplexity();
    }
}
