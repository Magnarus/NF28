using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MagicEffectContainer))]
public class Enchantment : MonoBehaviour {
    public MagicEffect effect;

    MagicEffectInstance myEffect;
	// Use this for initialization
	void Start () {
        myEffect = this.GetComponent<MagicEffectContainer>().applyEffect(effect, this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnDestroy()
    {
        this.GetComponent<MagicEffectContainer>().dispellEffect(myEffect);
    }
}
