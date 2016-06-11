using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Descriptors;

public class VictoryConditionAgent : BattleState {
    BattleController owner;
    List<Creature> listJ1;
    List<Creature> listJ2;


	void Enter() {
		base.Enter ();
		checkGameState ();
	}

    // Use this for initialization
    void Start () {
        owner = GetComponent<BattleController>();
	}

    void Awake()
    {
        owner = GetComponent<BattleController>();
    }


    public void checkGameState() {
        listJ1 = owner.creaturesJ1;
        listJ2 = owner.creaturesJ2;
        if (!heroDead() && !annihilation()) {
            owner.ChangeState<SelectUnitState>(); // On reprend le jeu
        }   
        else {
            owner.ChangeState<ResumeState>();
        }

    }

    /** Check la condition de victoire hero mort **/
    private bool heroDead() {
        
        foreach(Creature c in listJ1) {
            if(c.classCreature == "hero") {
                if(c.GetComponent<CreatureDescriptor>().HP.value == 0) {
                    return true;
                } else {
                    break;
                }
            }
        }

        foreach (Creature c in listJ2)
        {
            if (c.classCreature == "hero")
            {
                if (c.GetComponent<CreatureDescriptor>().HP.value == 0)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }
        }

        return false;
    }

    private bool annihilation() 
    {
        return listJ1.Count == 0 || listJ2.Count == 0;
    }


}
