using UnityEngine;
using System.Collections;
using Descriptors;
using System.Collections.Generic;

public class AttackBehaviour : AgentBehaviour {

	public AttackBehaviour (Agent myAgent) : base (myAgent) {	}


	public override CreatureAction Run() {
		List<PhysicTile> tiles;
		tiles = GetTilesInRange ();
		List<PhysicTile> ennemies = GetEnnemyInRange (tiles);
		float damages = 0; 
		float maxDamage = 0;
		Creature targetDeath = null;
		Creature targetDamages = null;

		CreatureDescriptor statsCreature = ((AgentCreature)Parent).CurrentCreature.GetComponent<CreatureDescriptor>();

		//Debug.Log ("Pour l'agent : " + ((AgentCreature)Parent).CurrentCreature.classCreature + " les ennemis sont : " + ennemies.Count);

		foreach (PhysicTile e in ennemies) {
			Creature newC = e.contentTile.GetComponent<Creature> ();
			CreatureDescriptor statsEnnemy = newC.GetComponent<CreatureDescriptor> ();
			damages += (20 + statsCreature.Strength.value - statsEnnemy.Armor.value);
			if (statsEnnemy.HP.CurrentValue - damages <= 0) {
				if (targetDeath == null || newC.classCreature == "hero") {
					targetDeath = newC;
				}
			} else if (damages > maxDamage) {
				maxDamage = damages;
				targetDamages = newC;
			}
		}
		PhysicTile dest;
		CreatureAction c = null;
		if (targetDeath != null) {
			 dest = GetDirectionToGo (targetDeath.tile);
			c = new CreatureAction (ActionType.ATK, ((AgentCreature)Parent).CurrentCreature, dest, targetDeath, 500); 
			SavePath (((AgentCreature)Parent).CurrentCreature.tile, dest);
		} else if (targetDamages != null) {
			dest = GetDirectionToGo (targetDamages.tile);
			c = new CreatureAction (ActionType.ATK, ((AgentCreature)Parent).CurrentCreature, dest , targetDamages, maxDamage); 
			SavePath (((AgentCreature)Parent).CurrentCreature.tile, dest);

		} 
		return c;
	}


	public override bool finish() {
		return true;
	}


	public List<PhysicTile> GetTilesInRange() {
		// Récupère les capacités de mouvement de la créature
		Movement m = ((AgentCreature)Parent).CurrentCreature.gameObject.GetComponent<Movement> ();
		// Listes des cases sur lesquelles elle peut se déplacer
		List<PhysicTile> tiles = m.GetTilesInRange (Parent.controller.board);
		targetMovement = tiles;
		List<PhysicTile> maxRangeTiles = Parent.controller.board.GetMaxRange(((AgentCreature)Parent).CurrentCreature, tiles);

		/*if (((AgentCreature)Parent).CurrentCreature.classCreature == "archer") {
			foreach (PhysicTile t in maxRangeTiles) {
				Debug.Log ("Peut tapper " + t.pos);
			}
		}*/

		return maxRangeTiles;
	}


	public List<PhysicTile> GetEnnemyInRange(List<PhysicTile> tiles) {
		List<PhysicTile> ennemies = new List<PhysicTile> ();
		List<Creature> ennemiesJ1 = Parent.controller.creaturesJ1;

		foreach (PhysicTile t in tiles) {
			if (t.contentTile != null && ennemiesJ1.Contains(t.contentTile.GetComponent<Creature>())) {
				ennemies.Add (t);
			}
		}
		return ennemies;
	}

	public virtual PhysicTile GetDirectionToGo(PhysicTile t) { return null; }





}
