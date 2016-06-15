using UnityEngine;
using System.Collections;
using Descriptors;
using System.Collections.Generic;

public class WarriorAttackBehaviour : AttackBehaviour {

	public WarriorAttackBehaviour(AgentCreature agent) : base(agent) {	}


	/** Retourne une des cases à portée d'attaque du warrior pour attaquer sa cible **/
	public override PhysicTile GetDirectionToGo(PhysicTile t) {
		PhysicTile destination = null;
		PhysicTile toTest;
		Point[] dirs = new Point[4] {  new Point(0, 1),  new Point(1, 0), new Point(0, -1), new Point(-1, 0) };
		int i = 0;
		while (i < 4 && destination == null) {
			if (Parent.controller.board.tiles.ContainsKey (t.pos + dirs [i])) {
				toTest = Parent.controller.board.tiles [t.pos + dirs [i]];
				if (TargetMovement.Contains (toTest) && toTest.contentTile == null)
					destination = toTest;
			}
			i++;
		}
		return destination;	
	}
}
