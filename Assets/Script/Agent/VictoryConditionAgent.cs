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
		string winner;
		winner = annihilation ();
		winner = heroDead ();
		if (winner != "") {        
			Debug.Log ("Ouiiiii!!" + winner);
			owner.matchController.localPlayer.CmdSyncVictory (winner);
        }   
        else {
			owner.ChangeState<SelectUnitState>(); // On reprend le jeu
        }

    }

    /** Check la condition de victoire hero mort **/
    private string heroDead() {
        
        foreach(Creature c in listJ1) {
            if(c.classCreature == "hero") {
                if(c.GetComponent<CreatureDescriptor>().HP.CurrentValue == 0) {
                    return "J2";
                } 
            }
        }

        foreach (Creature c in listJ2)
        {
            if (c.classCreature == "hero")
            {
				if (c.GetComponent<CreatureDescriptor>().HP.CurrentValue == 0)
                {
                    return "J1";
                }
             }
        }

        return "";
    }

    private string annihilation() 
    {
		if (listJ1.Count == 0)
			return "J2";
		else if( listJ2.Count == 0) return "J1";

		return "";
    }


}
