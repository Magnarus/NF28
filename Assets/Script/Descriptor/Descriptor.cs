using UnityEngine;
using System.Collections;
using Game;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class Descriptor : MonoBehaviour, IEnumerable<KeyValuePair<string, GameValue<float>>> {
    [SimpleGameValue]
    public GameValue<string> Name;

    //TODO generic enumerator type (duno how to do)
    public virtual IEnumerator<KeyValuePair<string, GameValue<float>>> GetEnumerator()
    {
        var type = this.GetType();
        return type.GetFields()
            .Where(x => x.FieldType.GetGenericTypeDefinition() == typeof(GameValue<>))
            .Where(x => x.FieldType.GetGenericArguments()[0] == typeof(float))
            .Select(x => new KeyValuePair<string, GameValue<float>>(x.Name, x.GetValue(this) as GameValue<float>))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
