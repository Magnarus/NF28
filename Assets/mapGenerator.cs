using UnityEngine;
using System.Collections;

public class mapGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var go= GameObject.Find("00");
        var component = this.GetComponent<GridMap>();
        for (int i =0; i < 100; ++i)
        {
            component.tiles[i] = go.GetComponent<Tile>();
            System.Console.WriteLine(component.tiles[i].ToString());
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
