using UnityEngine;
using System.Collections;

public class ArcherAttackBehaviour : AttackBehaviour {


	public ArcherAttackBehaviour(AgentCreature agent) : base(agent) {	}


	/** Ici on cherche à effectuer son action en restant le plus loin possible **/
	public override PhysicTile GetDirectionToGo (PhysicTile t)
	{
		Creature current = ((AgentCreature)Parent).CurrentCreature;
		PhysicTile destination = null;
		PhysicTile toTest;
		GameObject ability = current.GetComponentInChildren<AbilityRangeCalculator>().gameObject;
		AbilityRangeCalculator abilityRange = ability.GetComponent<AbilityRangeCalculator>();
		int i;

		int minI = t.pos.x;
		if (current.tile.pos.x < t.pos.x) {
			i = t.pos.x - abilityRange.horizontal;
		} else {
			i = t.pos.x + abilityRange.horizontal;
		}

		int maxJ = t.pos.y;
		int minJ = t.pos.y;
		bool inc = (minI > i);
		int j; 

		while(i != minI && destination == null) {
			j = minJ;
			while(j <= maxJ && destination == null) {
//				Debug.Log (" ( " + i + " ; " + j + " )");
				if (Parent.controller.board.tiles.ContainsKey (new Point(i, j))) {
					toTest = Parent.controller.board.tiles [new Point(i, j)];
					if (TargetMovement.Contains (toTest) && toTest.contentTile == null) {
						destination = toTest; break;
					}
				}
				j++;
			}
			minJ--;
			maxJ++;
			i += inc ? 1 : -1;
		}//*/
		//Debug.Log("Destination : " + destination.pos);
		return destination;
	}

}
