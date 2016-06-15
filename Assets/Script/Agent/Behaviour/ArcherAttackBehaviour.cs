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
		int i = current.tile.pos.x + abilityRange.horizontal;
		int minI = current.tile.pos.x;
		int maxJ = current.tile.pos.y + abilityRange.horizontal;
		int minJ = current.tile.pos.y - abilityRange.horizontal;
		int currentX, currentY;
		int j; 

		while(i >= minI && destination == null) {
			currentX = minI + (i - minI);
			minJ--;
			maxJ++;
			j = minJ;
			while(j <= maxJ && destination == null) {
				if (Parent.controller.board.tiles.ContainsKey (new Point(i, j))) {
					toTest = Parent.controller.board.tiles [new Point(i, j)];
					if (TargetMovement.Contains (toTest) && toTest.contentTile == null) {
						destination = toTest; break;
					}
				}				
			}
			i--;
		}
		return destination;
	}

}
