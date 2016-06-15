﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBehaviour : AgentBehaviour {

	private List<PhysicTile> targetMovement;
	public List<PhysicTile> TargetMovement {
		get {
			return targetMovement;
		}
	}

	public AttackBehaviour(Agent myAgent) {
		Parent = myAgent;
	}


	public override CreatureAction Run() {
		return null;
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
		List<PhysicTile> maxRangeTiles = Parent.controller.board.GetMaxRange((AgentCreature)Parent, tiles);

		return maxRangeTiles;
	}


	public List<PhysicTile> GetEnnemyInRange(List<PhysicTile> tiles) {
		List<PhysicTile> ennemies = new List<PhysicTile> ();

		foreach (PhysicTile t in tiles) {
			if (t.contentTile != null) {
				ennemies.Add (t);
			}
		}
		return ennemies;
	}


	public virtual PhysicTile GetDirectionToGo(PhysicTile t) { return null; }
}
