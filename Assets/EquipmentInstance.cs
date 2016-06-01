using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

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

/*
 * Equipment _modifieditem = new Equipment();
    public Equipment modifiedItem
    {
        get
        {
            foreach (var prop in properties)
            {
                var currentValue = parseProperty(baseItem, prop.Key);
                //TODO: null is bad. find another special flag
                if (currentValue != null)
                    parseProperty(_modifieditem, prop.Key, prop.Value);
            }

            return _modifieditem;
        }
    }

    object parseProperty(object source, string key, object value = null)
    {
        if (key.Contains("."))
        {
            var split = key.Split('.');
            var first = split[0];
            var next = string.Join(".", split.Skip(1).ToArray());
            var currentobj = parseProperty(source, first);
            if (currentobj == null)
                return currentobj;
            else
                return parseProperty(currentobj, next, value);
        }
        else
        {
            var myType = source.GetType();
            if (key.StartsWith("`"))
            {
                key = key.Substring(1);

                //TODO omygad that's slow
                var type = AppDomain.CurrentDomain.GetAssemblies()
                                   .SelectMany(x => x.GetTypes())
                                   .FirstOrDefault(x => x.Name == "MyClass");

                return ((MonoBehaviour)source).GetComponent(type);
            }
            else
            {
                var member = myType.GetMember(key);

                if (member.Length > 1)
                    System.Console.WriteLine("Warning: membre multiple détecté pour la substitution de " + key);

                if (member.Length <= 0)
                    return null;

                var myMember = member.First();
                switch (myMember.MemberType)
                {
                    case MemberTypes.Field:
                        if (value == null)
                            return ((FieldInfo)myMember).GetValue(source);
                        else
                            ((FieldInfo)myMember).SetValue(source, value);
                        break;
                    case MemberTypes.Property:
                        if (value == null)
                            return ((PropertyInfo)myMember).GetValue(source, new object[] { });
                        else
                            ((PropertyInfo)myMember).SetValue(source, value, new object[] { });
                        break;

                        //TODO: overloading functions
                }
                return null;
            }
        }
    }
*/