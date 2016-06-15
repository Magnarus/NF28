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

		List<PhysicTile> maxRangeTiles = GetMaxRange (tiles);



		return null;
	}

	private List<PhysicTile> GetMaxRange(List<PhysicTile> tiles) {
		// Calcul de la range des cases extrêmes
		List<PhysicTile> returnedList = new List<PhysicTile>(tiles);
		Hashtable table = new Hashtable();

		int xMax = -1; 
		int xMin = 9999;
		// Récupération des tiles extrêmes
		foreach(PhysicTile t in tiles) {
			if (!table.ContainsKey (t.pos.x)) {
				table.Add (t.pos.x, new List<PhysicTile> ());
			} else {
				if (((List<PhysicTile>)table[t.pos.x])[0].pos.y > t.pos.y) { 
					// C'est le plus éloigné sur la ligne à gauche
					((List<PhysicTile>)table[t.pos.x])[0] = t;

				} else if (((List<PhysicTile>)table[t.pos.x])[1].pos.y <= t.pos.y) {
					((List<PhysicTile>)table[t.pos.x])[1] = t;
				}
			}
			if (xMin > t.pos.x)
				xMin = t.pos.x;
			if (xMax < t.pos.x)
				xMax = t.pos.x;
		}
		// On récupère la portée de l'unité
		GameObject ability = ((AgentCreature)Parent).CurrentCreature.GetComponentInChildren<AbilityRangeCalculator>().gameObject;
		AbilityRangeCalculator abilityRange = ability.GetComponent<AbilityRangeCalculator>();
		// Calcul de leurs portées
		foreach (int i in table.Keys) {
			List<PhysicTile> tmp = abilityRange.GetTilesInRange (Parent.controller.board, ((List<PhysicTile>)table[i])[0]);
			List<PhysicTile> tmpBis = (abilityRange.GetTilesInRange (Parent.controller.board, ((List<PhysicTile>)table [i]) [0]));
			tmp.AddRange (tmpBis);
			foreach (PhysicTile t in tmp) {
				if (!returnedList.Contains(t)) {
					returnedList.Add (t);
				}			
			}	
		}

		// Et on ajoute les tiles max et min
		int yMin = ((List<PhysicTile>)table[xMin])[0].pos.y;
		int yMax = ((List<PhysicTile>)table [xMin]) [1].pos.y;
		for (int i = yMin; i < yMax; i++) {
			List<PhysicTile> tmp = abilityRange.GetTilesInRange (Parent.controller.board, ((List<PhysicTile>)table[i])[0]);
			foreach (PhysicTile t in tmp) {
				if (!returnedList.Contains(t)) {
					returnedList.Add (t);
				}			
			}	
		}
		// Et on ajoute les tiles max et min
		yMin = ((List<PhysicTile>)table[xMax])[0].pos.y;
		yMax = ((List<PhysicTile>)table [xMax]) [1].pos.y;
		for (int i = yMin; i < yMax; i++) {
			List<PhysicTile> tmp = abilityRange.GetTilesInRange (Parent.controller.board, ((List<PhysicTile>)table[i])[0]);
			foreach (PhysicTile t in tmp) {
				if (!returnedList.Contains(t)) {
					returnedList.Add (t);
				}			
			}	
		}
		return returnedList;
	}
		
}
