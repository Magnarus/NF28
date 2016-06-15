using UnityEngine;
using System.Collections;

public class HeroAttackBehaviour : AttackBehaviour {

	public HeroAttackBehaviour(Agent agent) : base(agent) { }

	public override PhysicTile GetDirectionToGo (PhysicTile t)
	{
		PhysicTile destination = null;
		PhysicTile toTest;
		Point[] dirs = new Point[8] {  new Point(0, 1),  new Point(1, 0), new Point(0, -1), new Point(-1, 0),
									   new Point(0,2), new Point(2,0), new Point(0, -2), new Point(-2,0)  };
		int i = 0;
		while (i < 8 && destination == null) {
			if (Parent.controller.board.tiles.ContainsKey (t.pos + dirs [i])) {
				toTest = Parent.controller.board.tiles [t.pos + dirs [i]];
				if (TargetMovement.Contains (toTest) && toTest.contentTile == null)
					destination = toTest;
			}
			i++;
		}
		return destination;	
	}
}
