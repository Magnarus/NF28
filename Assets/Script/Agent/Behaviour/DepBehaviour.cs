using System;
using System.Collections.Generic;
public class DepBehaviour : AgentBehaviour
{
	public DepBehaviour ()
	{
		Movement mov = ((AgentCreature)MParent).CurrentCreature.gameObject.GetComponent<Movement> ();
		List<PhysicTile> tile = mov.GetTilesInRange();
	}

	public override bool Run ()
	{
		TileInfoList infoList = new TileInfoList ();
		List<Creature> opponentCreature = MParent.controller.creaturesJ1;
		Movement mov;
		List<PhysicTile> creatureTileRange;
		foreach(Creature c in opponentCreature) {
			mov = c.gameObject.GetComponent<Movement> ();
			creatureTileRange = mov.GetTilesInRange (MParent.controller.board);
			//TODO add attack tile range
			foreach (PhysicTile tile in creatureTileRange) {
				infoList.addTile (tile, c);
			}
		}
		Creature current = ((AgentCreature)MParent).CurrentCreature;
		mov = current.gameObject.GetComponent<Movement> ();
		List<PhysicTile> currentCreatureMovTileRange = mov.GetTilesInRange(MParent.controller.board);
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

