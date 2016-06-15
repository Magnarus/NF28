using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AgentBehaviour {

	private Agent mParent;
	public Agent Parent {
		get { return mParent; }
		set { mParent = value; }
	}
	protected List<PhysicTile> targetMovement;
	public List<PhysicTile> TargetMovement {
		get {
			return targetMovement;
		}
	}

	public List<Point> chemin;

	public List<Point> Chemin {
		get {
			return chemin;
		}
	}

	public AgentBehaviour(Agent myAgent) {
		Parent = myAgent;
		chemin = new List<Point> ();
	}

	public abstract CreatureAction Run();

	public virtual bool finish() {
		return true;
	}

	// Parcourir le résultat de la fin au début
	public void SavePath(PhysicTile from, PhysicTile to) {
		PhysicTile current = to;
		chemin.Clear ();
		while (current != from) {
				chemin.Add(current.pos);
				current = current.prev;
		}
	}

	public void RecreatePath() {
		List<Point> p = chemin;
		Point last = p [p.Count - 1];
		for (int i = p.Count-2 ; i >= 0; i--) {
			Parent.controller.board.tiles[last].prev = Parent.controller.board.tiles[p[i]];
			last = Parent.controller.board.tiles[p[i]].pos;
		}
	}
}
