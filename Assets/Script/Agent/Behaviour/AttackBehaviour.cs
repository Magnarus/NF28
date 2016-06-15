using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBehaviour : AgentBehaviour {


	public AttackBehaviour(Agent myAgent) {
		Parent = myAgent;
	}



	public override bool Run() {
		return true;
	}


	public override bool finish() {
		return true;
	}


	public List<PhysicTile> GetTilesInRange() {
		// Récupère les capacités de mouvement de la créature
		Movement m = ((AgentCreature)Parent).CurrentCreature.gameObject.GetComponent<Movement> ();
		// Listes des cases sur lesquelles elle peut se déplacer
		List<PhysicTile> tiles = m.GetTilesInRange (Parent.controller.board);

		List<PhysicTile> maxRangeTiles = Parent.controller.board.GetMaxRange(Parent, tiles);

		return null;
	}


		
}
