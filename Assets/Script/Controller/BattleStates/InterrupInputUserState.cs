using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterruptUserInputState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        if (!canvas)
        {
            GameObject parent = GameObject.Find("Canvas");
            foreach (Transform child in parent.transform)
            {
                if (child.name == "Left")
                {
                    canvas = child.gameObject;
                    break;
                }
            }
        }
        canvas.SetActive(false);
        StartCoroutine("Sequence");
    }

    IEnumerator Sequence()
    {
		owner.matchController.localPlayer.CmdSyncPosition (getChemin(owner.currentTile));
        turn.hasUnitMoved = true;
        Movement m = turn.currentCreature.GetComponent<Movement>();
        yield return StartCoroutine(m.Traverse(owner.currentTile));
        turn.currentCreature.Match();
        owner.ChangeState<SelectUnitState>();
    }

	Point[] getChemin(PhysicTile tile) {
		List<Point> myPoints = new List<Point> ();
		while (tile.prev != null) {
			myPoints.Add(tile.pos);
			tile = tile.prev;
		}
		myPoints.Add (tile.pos);
		return myPoints.ToArray();
	}
}