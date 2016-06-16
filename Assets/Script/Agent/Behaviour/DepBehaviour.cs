using System;
using System.Collections.Generic;
using UnityEngine;
using Descriptors;

public class DepBehaviour : AgentBehaviour
{
	List<Creature> opponentCreatures = new List<Creature>();
	Creature current;

	public DepBehaviour (Agent agent) : base (agent)
	{
		
	}

	public override CreatureAction Run ()
	{
		//On récupère la portée de tous les ennemis
		TileInfoList infoList = new TileInfoList ();
		opponentCreatures = Parent.controller.creaturesJ1;
		Movement mov;
		List<PhysicTile> creatureTileRange = new List<PhysicTile>();
		List<PhysicTile> totalRange = new List<PhysicTile> ();
		current = ((AgentCreature)Parent).CurrentCreature;
		foreach(Creature c in opponentCreatures) {
			creatureTileRange.Clear ();
			totalRange.Clear ();
			mov = c.gameObject.GetComponent<Movement> ();
			creatureTileRange = mov.GetTilesInRange (Parent.controller.board);
			GameObject ability = ((AgentCreature)Parent).CurrentCreature.GetComponentInChildren<AbilityRangeCalculator>().gameObject;
			totalRange = Parent.controller.board.GetMaxRange ((AgentCreature)Parent, creatureTileRange);
			Debug.Log ("cases de : " + c.classCreature);
			foreach (PhysicTile tile in totalRange) {
				Debug.Log (tile.pos);
				infoList.addTile (current, tile, c);
			}
		}
			
		//Récupère la zone de déplacement de la creature actuelle.
		mov = current.gameObject.GetComponent<Movement> ();
		List<PhysicTile> currentCreatureMovTileRange = mov.GetTilesInRange(Parent.controller.board);

		//On enlève la case sur laquelle est déjà la créature pour le test de déplacement.
		currentCreatureMovTileRange.Remove (current.tile);
		PhysicTile safestTile = getSafestTile (infoList, currentCreatureMovTileRange);
		if (current.classCreature == "warrior")
			Debug.Log ("case choisie : " + safestTile.pos);
		//Pas besoin de faire d'action de fuite : on reste sur place.
		if (safestTile == current.tile)
			return null;
		
		CreatureAction action = new CreatureAction (ActionType.DEP, current, safestTile);
		SavePath (current.tile, safestTile);
		return action;
	}

	/*
	 * Méthode qui récupère la case la plus sure possible
	 * Si on trouve une case qui n'est atteignable par personne, on la retourne directement
	 * Sinon, on prend la case pour laquelle le moins de dégâts totaux pourront être infligés
	 */
	private PhysicTile getSafestTile(TileInfoList infoList, List<PhysicTile> tiles) {
		TileInfo safest = infoList.getTileInfo(tiles[0]);

		List<PhysicTile> availableTiles = new List<PhysicTile> ();
		//Si la première case n'est pas une case atteignable par les ennemis
		if(safest == null) {
			if (current.classCreature == "warrior")
				Debug.Log ("pas de dégâts en " + tiles [0].pos);
			availableTiles.Add (tiles [0]);
		}
		int cpt = 1;
		TileInfo currentTileInfo;
		while (cpt < tiles.Count) {
			currentTileInfo = infoList.getTileInfo(tiles[cpt]);
			//Si la case courante n'est pas une case atteignable par les ennemis
			if(currentTileInfo == null) {
				if(current.classCreature == "warrior")
					Debug.Log ("pas de dégâts en " + tiles [cpt].pos);
				availableTiles.Add(tiles[cpt]);
			}
			//Si la case courante fera moins mal que la case la plus sure actuelle
			else if(safest == null || currentTileInfo.Damages < safest.Damages) {
				safest = currentTileInfo;
			}
			cpt++;
		}

		//On récupère les info sur la case de la créature fuyant
		TileInfo creatureTileInfo = infoList.getTileInfo(current.tile);

		//Si il y a des cases sans dégâts, on prend la plus proche de l'ennemi le plus vulnérable
		if (availableTiles.Count > 0) {
			Creature weakest = getWeakest (opponentCreatures);
			if (current.classCreature == "warrior")
				Debug.Log ("YALALA");
			return getClosestTile (weakest.tile, tiles);
		} 
		// Sinon si la case sur laquelle est le perso est safe ou équivalente à la moins dangereuse
		else if (creatureTileInfo == null || creatureTileInfo.Damages <= safest.Damages) {
			if (current.classCreature == "warrior")
				Debug.Log ("YOLOLO");
			return current.tile;
		}
		if (current.classCreature == "warrior")
			Debug.Log ("YULULU");
		//Sinon, ben on prend le moins dangereux de ce qui reste.
		return safest.Tile;
	}

	/**
	 * Fonction qui retourne l'ennemi le plus faible
	 * Si on considère que le hero est mort, on le retourne
	 * Sinon si un ennemi peut mourir, on retourne le premier.
	 * Sinon, on retourne celui qui peut se prendre le plus de dégâts
	 */
	private Creature getWeakest(List<Creature> ennemies) {
		List<Creature> deadCreatures = new List<Creature> ();
		Creature weakest = ennemies [0];
		int cpt = 1;
		CreatureDescriptor statsCreature = current.GetComponent<CreatureDescriptor>();
		CreatureDescriptor statsEnnemi = ennemies [0].GetComponent<CreatureDescriptor> ();
		float weakestDamages = (20 + statsCreature.Strength.value - statsEnnemi.Armor.value);
		if (statsEnnemi.HP.CurrentValue - weakestDamages <= 0) {
			if (weakest.classCreature == "hero")
				return weakest;
			else
				deadCreatures.Add (weakest);
		}
		float tmpDamages = 0;
		while (cpt < ennemies.Count) {
			statsEnnemi = ennemies[cpt].GetComponent<CreatureDescriptor> ();
			tmpDamages = (20 + statsCreature.Strength.value - statsEnnemi.Armor.value);
			if (weakestDamages > tmpDamages) {
				weakestDamages = tmpDamages;
				weakest = ennemies [cpt];
				if (statsEnnemi.HP.CurrentValue - weakestDamages <= 0) {
					if (weakest.classCreature == "hero")
						return weakest;
					else
						deadCreatures.Add (weakest);
				}
			}
			cpt++;
		}
		if (deadCreatures.Count > 0)
			return deadCreatures [0];
		else
			return weakest;
	}

	/**
	 * Fonction qui retourne la PhysicTile de tiles la plus proche de start
	 */
	private PhysicTile getClosestTile(PhysicTile start, List<PhysicTile> tiles) {
		PhysicTile closest = tiles[0];
		int distance = Math.Abs (tiles [0].pos.x - start.pos.x) + Math.Abs(tiles[0].pos.y - start.pos.y);
		int cpt = 1;
		int currDistance;
		while (cpt < tiles.Count) {
			currDistance = Math.Abs (tiles[cpt].pos.x - start.pos.x) + Math.Abs (tiles[cpt].pos.y - start.pos.y);
			if (distance > currDistance) {
				distance = currDistance;
				closest = tiles [cpt];
			}
			cpt++;
		}
		return closest;
	}
}
