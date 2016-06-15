using System;
using System.Collections.Generic;
using UnityEngine;

public class DepBehaviour : AgentBehaviour
{
	public DepBehaviour ()
	{
		Movement mov = ((AgentCreature)Parent).CurrentCreature.gameObject.GetComponent<Movement> ();
		List<PhysicTile> tile = mov.GetTilesInRange();
	}

	public override bool Run ()
	{
		TileInfoList infoList = new TileInfoList ();
		List<Creature> opponentCreature = Parent.controller.creaturesJ1;
		Movement mov;
		List<PhysicTile> creatureTileRange;
		foreach(Creature c in opponentCreature) {
			mov = c.gameObject.GetComponent<Movement> ();
			creatureTileRange = mov.GetTilesInRange (Parent.controller.board);
			GameObject ability = ((AgentCreature)Parent).CurrentCreature.GetComponentInChildren<AbilityRangeCalculator>().gameObject;
			List<PhysicTile> totalRange = Parent.controller.board.GetMaxRange (Parent, creatureTileRange);

			foreach (PhysicTile tile in totalRange) {
				infoList.addTile (tile, c);
			}
		}
		Creature current = ((AgentCreature)Parent).CurrentCreature;
		mov = current.gameObject.GetComponent<Movement> ();
		List<PhysicTile> currentCreatureMovTileRange = mov.GetTilesInRange(Parent.controller.board);
		PhysicTile saferTile = getSafestTile (infoList, currentCreatureMovTileRange);
		CreatureAction action = new CreatureAction (ActionType.DEP, current, saferTile);
		//TODO send it to agent
	}

	/*
	 * Méthode qui récupère la case la plus sure possible
	 * Si on trouve une case qui n'est atteignable par personne, on la retourne directement
	 * Sinon, on prend la case pour laquelle le moins de dégâts totaux pourront être infligés
	 */
	private PhysicTile getSafestTile(TileInfoList infoList, List<PhysicTile> tiles) {
		TileInfo safest = infoList.getTileInfo(tiles[0]);
		//Si la première case n'est pas une case atteignable par les ennemis
		if(safest == null) {
			return tiles[0];
		}
		else {
			int cpt = 1;
			TileInfo currentTileInfo;
			while (cpt < tiles.Count) {
				currentTileInfo = infoList.getTileInfo(tiles[cpt]);
				//Si la case courante n'est pas une case atteignable par les ennemis
				if(currentTileInfo == null) {
					return tiles[cpt];
				}
				//Si la case courante fera moins mal que la case la plus sure actuelle
				if(currentTileInfo.Damages < safest.Damages) {
					safest = currentTileInfo;
				}
				cpt++;
			}
			return safest.Tile;
		}
	}
}

