using UnityEngine;
using System.Collections;

public class affichageMenuUI : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        //menu State
        GameObject.Find("Right").SetActive(false);


        //menu action
        GameObject.Find("Left").SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //var myObject = GameObject.Find("BattleController").;
        if (!myObject) return;
        else
        {

            //menu State
            GameObject.Find("Right").SetActive(true);


            //menu action
            GameObject.Find("Left").SetActive(true);


        }


    }
}
