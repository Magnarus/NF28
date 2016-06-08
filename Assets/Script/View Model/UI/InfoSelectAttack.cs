using UnityEngine;
using System.Collections;

public class InfoSelectAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {

       

	}
    public void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Selectionner le hero a Attaquer");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
