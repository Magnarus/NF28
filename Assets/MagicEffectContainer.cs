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

    public MagicEffectInstance applyEffect(MagicEffect effect, GameObject source)
    {
        var instance = new MagicEffectInstance();
        instance.target = this.gameObject;
        instance.baseEffect = effect;
        effect.Apply(instance, this.gameObject, source);
        magicEffects.Push(instance);
        return instance;
    }

    public void dispellEffect(MagicEffectInstance i)
    {
        dispellEffects(new MagicEffectInstance[] { i });
    }

    //internal dispell
    public void dispellEffects(IEnumerable<MagicEffectInstance> list, GameObject source = null)
    {
        source = source ?? this.gameObject;

        foreach (var item in list)
        {
            item.dispell(source);
        }
        magicEffects = magicEffects.Except(list);
        addComplexity();
    }

    //Dispell spells
    public void dispellEffects(MagicTag tags, GameObject source = null, int limit = -1)
    {

        var dispellable = magicEffects
            .Where(x => (tags & x.baseEffect.Description) != 0);

        var dispelled = (limit >= 0 ? dispellable.Take(limit) : dispellable).ToArray();

        if (dispelled.Count() <= 0)
            return;

        dispellEffects(dispelled, source);
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
