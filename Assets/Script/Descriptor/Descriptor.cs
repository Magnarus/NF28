using UnityEngine;
using System.Collections;
using Game;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class Descriptor : MonoBehaviour, 
    IEnumerable<KeyValuePair<string, FloatGameValue>>,
    ISerializationCallbackReceiver
{
    [SimpleGameValue]
    [SerializeField]
    public StringGameValue Name;

    Type reflectionType;

    public Descriptor()
    {
        Name = new StringGameValue();
        reflectionType = this.GetType();
    }

    //TODO generic enumerator type (duno how to do)
    public virtual IEnumerator<KeyValuePair<string, FloatGameValue>> GetEnumerator()
    {
        var type = this.GetType();
        return this.AsDictionary(default(float)).Cast<KeyValuePair<string, FloatGameValue>>().GetEnumerator();
    }

    public Dictionary<string, GameValue<T>> AsDictionary<T>(T infere = default(T)) where T:IComparable
    {
        var type = reflectionType;
        return type.GetFields()
            .Where(x => (x.FieldType.IsGenericType ? x.FieldType : x.FieldType.BaseType.IsGenericType ? x.FieldType.BaseType : typeof(List<string>)).GetGenericTypeDefinition() == typeof(GameValue<>))
            .Where(x => (x.FieldType.IsGenericType ? x.FieldType : x.FieldType.BaseType).GetGenericArguments()[0] == typeof(T))
            .ToDictionary(
            x => x.Name,
            x => x.GetValue(this) as GameValue<T>);
    }

    public void OnAfterDeserialize()
    {
        foreach (var item in this)
            Debug.Log(Name + "." + item.Key + ": " + item.Value.value);
        foreach (var item in this.AsDictionary(default(int)))
            Debug.Log(Name + "." + item.Key + ": " + item.Value.value);
    }

    public void OnBeforeSerialize()
    {
        
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
