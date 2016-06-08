using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Descriptors;

public class LoadIcone : MonoBehaviour {

	// Use this for initialization
    private Image icone;


    void Update()
    {
        refreshIcone();


    }

    public void refreshIcone()
    {
        icone= GameObject.Find("Guerrier").GetComponent<Image>();
        var myObject = GameObject.Find("BattleController").GetComponent<BattleController>().currentTile.contentTile;
        if (!myObject) return;
        icone.sprite = myObject.GetComponent<Creature>().Icone;

          
        
    }




}
