using System;
using System.Collections.Generic;

/**
 * Classe utilitaire de manipulation pour le DepBehaviour
 * Permet d'encapsuler des opérations effectuées sur la liste
 */
public class TileInfoList
{
	private List<TileInfo> infos = new List<TileInfo>();

	public TileInfoList ()
	{
	}

	public void addTile(Creature current, PhysicTile tile, Creature c) {
		TileInfo info = getTileInfo (tile);
		if (info != null) {
			info.addOpponent (c);
		} else {
			info = new TileInfo(current, tile, c);
			infos.Add (info);
		}
	}

	public TileInfo getTileInfo(PhysicTile tile) {
		foreach (TileInfo ti in infos) {
			if (ti.Tile == tile) {
				return ti;
			}
		}
		return null;
	}
}

