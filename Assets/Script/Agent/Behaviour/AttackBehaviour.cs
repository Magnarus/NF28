using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBehaviour : AgentBehaviour {

	private Agent myAgent;


	public AttackBehaviour(Agent myAgent) {

	}



	public override bool Run() {
		Movement m = ((AgentCreature)myAgent).CurrentCreature.gameObject.GetComponent<Movement> ();
		List<PhysicTile> tiles = m.GetTilesInRange (myAgent.controller.board);
		return true;
	}


	public override bool finish() {



		return true;
	}
}
