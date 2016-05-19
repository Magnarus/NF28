using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class inactive : MonoBehaviour {
    public bool active;
    private bool lastActive;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().enabled = lastActive = active;
    }
	
	// Update is called once per frame
	void Update () {
        if (active != lastActive)
            this.GetComponent<SpriteRenderer>().enabled = lastActive = active;
    }
}
