using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EquipmentInstance
{
    public GameObject target;
    public Equipment baseItem;

    Dictionary<string, object> properties = new Dictionary<string, object>();

    public void Unequip()
    {
        baseItem.Unequip(this, target);
    }

    public object this[string key]
    {
        get
        {
            return properties[key];
        }
        set
        {
            if (properties.ContainsKey(key))
                properties[key] = value;
            else
                properties.Add(key, value);
        }
    }
}

