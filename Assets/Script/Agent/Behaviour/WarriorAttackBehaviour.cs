using UnityEngine;
using System.Collections;
using Descriptors;
using System.Collections.Generic;

public class WarriorAttackBehaviour : AttackBehaviour {

	public WarriorAttackBehaviour(AgentCreature agent) : base(agent) {	}


	public override CreatureAction Run() {
		List<Creature> ennemiesJ1 = Parent.controller.creaturesJ1;
		List<PhysicTile> tiles = new List<PhysicTile> ();
		tiles = GetTilesInRange ();

		List<PhysicTile> ennemies = GetEnnemyInRange (tiles);
		float damages = 0; 
		float maxDamage = 0;
		Creature targetDeath = null;
		Creature targetDamages = null;

		CreatureDescriptor statsCreature = ((AgentCreature)Parent).CurrentCreature.GetComponent<CreatureDescriptor>();

		foreach (PhysicTile e in ennemies) {
			Creature newC = e.contentTile;
			if (ennemiesJ1.Contains (newC)) {
				CreatureDescriptor statsEnnemy = newC.GetComponent<CreatureDescriptor> ();
				damages += (20 + statsCreature.Strength.value - statsEnnemy.Armor.value);

				if (statsEnnemy - damages <= 0) {
					if (targetDeath == null || newC.classCreature == "hero") {
						targetDeath = newC;
					}
				} else if (damages > maxDamage) {
					maxDamage = damages;
					targetDamages = newC;
				}
			}
		}

		CreatureAction c;
		if (targetDeath != null) {
			c = new CreatureAction (ActionType.ATK, ((AgentCreature)Parent).CurrentCreature, GetDirection (), targetDeath, 500); 
		} else {
			c = new CreatureAction (ActionType.ATK, ((AgentCreature)Parent).CurrentCreature, GetDirection (), targetDamages, maxDamage); 
		}

		return c;
	}

	public override PhysicTile GetDirection(PhysicTile t) {
		PhysicTile destination = null;
		PhysicTile toTest;
		Point[] dirs = new Point[4] {  new Point(0, 1),  new Point(1, 0), new Point(0, -1), new Point(-1, 0) };
		int i = 0;
		while (i < 4 && destination == null) {
			if (Parent.controller.board.tiles.ContainsKey (t.pos + dirs [i])) {
				toTest = Parent.controller.board.tiles [t.pos + dirs [i]];
				if (TargetMovement.Contains (toTest))
					destination = toTest;
			}
			i++;
		}
		return destination;	
	}
}
