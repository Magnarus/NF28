using System;
using System.Collections.Generic;
using Descriptors;
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

	public TileInfo(Creature current, PhysicTile tile, Creature c) {
		this.tile = tile;
		opponents.Add (c);
	}

	public void addOpponent(Creature current, Creature c) {
		if (!opponents.Contains (c)) {
			opponents.Add (c);
			updateDamage (current, c);
		}
	}

	private void updateDamage(Creature current, Creature newC) {
		CreatureDescriptor statsCreature = current.GetComponent<CreatureDescriptor>();
		CreatureDescriptor statsEnnemy = newC.GetComponent<CreatureDescriptor>();
		damages += (20 + statsCreature.Strength.value - statsEnnemy.Armor.value);
	}

}


