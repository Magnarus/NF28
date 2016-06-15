using System;
using System.Collections.Generic;

/*
 * Classe utilitaire du DepBehaviour
 * Contient pour une case atteignable par un ennemi
 * toutes les informations nécessaire à la réflexion de l'IA
 */
public class TileInfo
{
	private PhysicTile tile;
	public PhysicTile Tile {
		get { return tile; }
		set { tile = value; }
	}

	private List<Creature> opponents = new List<Creature> ();
	public int Count {
		get { return opponents.Count; }
	}

	private float damages = 0;
	public float Damages {
		get { return damages; }
		set { damages = value; }
	}

	public TileInfo(PhysicTile tile, Creature c) {
		this.tile = tile;
		opponents.Add (c);
	}

	public void addOpponent(Creature c) {
		if (!opponents.Contains (c)) {
			opponents.Add (c);
			updateDamage ();
		}
	}

	private void updateDamage() {
		//TODO update damages.
	}

}


