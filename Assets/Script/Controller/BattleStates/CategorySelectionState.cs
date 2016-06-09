using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CategorySelectionState : BattleState
{

    GameObject canvasBtn;
    public override void Enter() {
        if(!canvasBtn) { 
            GameObject canvas = GameObject.Find("Left");
            foreach (Transform child in canvas.transform) {
                
                if(child.name == "Action") {
                    
                    canvasBtn =  child.gameObject;
                    break;
                }
            }
        }
        foreach (Transform child in canvasBtn.transform)
        {
            string btnName = child.name;
            Button b = child.gameObject.GetComponent<Button>();
            if(turn.currentCreature.hasMoved && child.name == "Moove") {
                b.enabled = false;
            } else {
                b.enabled = true;
                b.onClick.AddListener(delegate { OnClick(btnName); });
            }
                
            
        }
    }

    void OnClick(string buttonName) {
        switch(buttonName) {
            case "Moove": owner.ChangeState<MoveTargetState>(); break;
            case "Stay":    
                    turn.currentCreature.hasFinished = true;
                    owner.ChangeState<SelectUnitState>(); break;
            case "Attack":
                turn.ability = turn.currentCreature.GetComponentInChildren<AbilityRangeCalculator>().gameObject;
                owner.ChangeState<AttackState>();
                break;
        } 

    }
}