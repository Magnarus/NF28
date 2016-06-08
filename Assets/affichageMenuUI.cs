using UnityEngine;
using System.Collections;

public class affichageMenuUI : MonoBehaviour {

    GameObject left;
    GameObject right;


    // Use this for initialization
    void Start()
    {
        left = GameObject.Find("Left");
        right = GameObject.Find("Right");
        //this.gameObject.SetActive(false);
        left.SetActive(false);
        right.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var myObject = GameObject.Find("BattleController").GetComponent<BattleController>().currentTile.contentTile;
        left.SetActive(myObject);
        right.SetActive(myObject);
    }
}
