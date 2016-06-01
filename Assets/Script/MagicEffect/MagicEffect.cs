using UnityEngine;

public class MagicEffect : MonoBehaviour
{
    [BitMask(typeof(MagicTag))]
    public MagicTag Description;

    public virtual bool Apply(MagicEffectInstance i, GameObject to, GameObject by)
    {
        var effectContainer = to.GetComponent<MagicEffectContainer>();
        if (!effectContainer) return false;

        //TODO: MagicTag based Vulnerability check

        return true;
    }

    public virtual bool Dispell(MagicEffectInstance i, GameObject from, GameObject by)
    {
        return true;
    }
}