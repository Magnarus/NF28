using System;
using System.Collections.Generic;
using UnityEngine;
using Descriptors;

public class DepBehaviour : AgentBehaviour
{
	List<Creature> opponentCreatures = new List<Creature>();
	Creature current;
	public DepBehaviour (Agent agent)
	{
		Parent = agent;
	}

	public override CreatureAction Run ()
	{
		//get all ennemy tile range
		TileInfoList infoList = new TileInfoList ();
		opponentCreatures = Parent.controller.creaturesJ1;
		Movement mov;
		List<PhysicTile> creatureTileRange;
		current = ((AgentCreature)Parent).CurrentCreature;
		foreach(Creature c in opponentCreatures) {
			mov = c.gameObject.GetComponent<Movement> ();
			creatureTileRange = mov.GetTilesInRange (Parent.controller.board);
			GameObject ability = ((AgentCreature)Parent).CurrentCreature.GetComponentInChildren<AbilityRangeCalculator>().gameObject;
			List<PhysicTile> totalRange = Parent.controller.board.GetMaxRange ((AgentCreature)Parent, creatureTileRange);
			foreach (PhysicTile tile in totalRange) {
				infoList.addTile (current, tile, c);
			}
		}

		if (infoList.getTileInfo (current.tile) == null)
			return null;

		//get currentCreature tile range and search safest case to be
		mov = current.gameObject.GetComponent<Movement> ();
		List<PhysicTile> currentCreatureMovTileRange = mov.GetTilesInRange(Parent.controller.board);
		PhysicTile safestTile = getSafestTile (infoList, currentCreatureMovTileRange);
		CreatureAction action = new CreatureAction (ActionType.DEP, current, safestTile);
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
			availableTiles.Add (tiles [0]);
		}
		else {
			int cpt = 1;
			TileInfo currentTileInfo;
			while (cpt < tiles.Count) {
				currentTileInfo = infoList.getTileInfo(tiles[cpt]);
				//Si la case courante n'est pas une case atteignable par les ennemis
				if(currentTileInfo == null) {
					availableTiles.Add(tiles[cpt]);
				}
				//Si la case courante fera moins mal que la case la plus sure actuelle
				if(currentTileInfo.Damages < safest.Damages) {
					safest = currentTileInfo;
				}
				cpt++;
			}

			//Si il y a des cases sans dégâts, on prend la plus proche de l'ennemi le plus vulnérable
			if (availableTiles.Count > 0) {
				Creature weakest = getWeakest (opponentCreatures);
				return getClosestTile (weakest.tile, tiles);
			}
		}
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
