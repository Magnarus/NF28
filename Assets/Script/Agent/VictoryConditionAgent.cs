using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Descriptors;

public class VictoryConditionAgent : BattleState {
    List<Creature> listJ1;
    List<Creature> listJ2;


	public override void Enter() {
		base.Enter ();
		checkGameState ();
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
                if(c.GetComponent<CreatureDescriptor>().HP.CurrentValue == 0) {
                    return true;
                } 
            }
        }

        foreach (Creature c in listJ2)
        {
            if (c.classCreature == "hero")
            {
				if (c.GetComponent<CreatureDescriptor>().HP.CurrentValue == 0)
                {
                    return true;
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
