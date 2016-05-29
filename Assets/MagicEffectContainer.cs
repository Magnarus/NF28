using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MagicEffectContainer : MonoBehaviour {
    const int recursiveComplexityLimit = 5;


    IEnumerable<MagicEffectInstance> magicEffects = new List<MagicEffectInstance>();
    int recursiveComplexity = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void addComplexity()
    {
        if (++recursiveComplexity > recursiveComplexityLimit)
        {
            magicEffects = magicEffects.ToArray();
            recursiveComplexity = 0;
        }
        
    }

    public void applyEffect(MagicEffect effect, GameObject source)
    {
        var instance = new MagicEffectInstance();
        effect.Apply(instance, this.gameObject, source);
        magicEffects.Push(instance);
    }

    public void dispellEffects(MagicTag tags, GameObject source = null, int limit = -1)
    {
        source = source ?? this.gameObject;

        var dispellable = magicEffects
            .Where(x => (tags & x.baseEffect.Description) != 0);

        var dispelled = (limit >= 0 ? dispellable.Take(limit) : dispellable).ToArray();
        
        foreach (var item in dispelled)
        {
            item.dispell(source);
        }


        magicEffects = magicEffects.Except(dispelled);
        addComplexity();
    }
}

public static class ExtensionMethods
{
    public static IEnumerable<T> Push<T>(this IEnumerable<T> self, T newItem)
    {
        foreach (var item in self) yield return item;
        yield return newItem;
    }
}
