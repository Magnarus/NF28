using System.Collections.Generic;
using UnityEngine;

public class MagicEffectInstance
{
    public GameObject target;
    public MagicEffect baseEffect;

    Dictionary<string, object> properties = new Dictionary<string, object>();

    public virtual void dispell(GameObject source)
    {
        baseEffect.Dispell(this, this.target, source);
        return;
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