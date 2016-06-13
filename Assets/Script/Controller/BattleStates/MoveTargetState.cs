using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class MoveTargetState : BattleState
{
    List<PhysicTile> tiles;
	bool reponse = false;

    public override void Enter()
    {
        base.Enter();
        Movement mover = turn.currentCreature.GetComponent<Movement>();
        tiles = mover.GetTilesInRange(board);
        board.SelectedColor(tiles);
    }

    public override void Exit()
    {
        base.Exit();
        board.NotSelectedColor(tiles);
        tiles = null;
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }


	void ValidationFunction(int windowId) {
		if (GUI.Button (new Rect(0,(Screen.height/3)/2,(Screen.width/3)/2,(Screen.height/3)/2), "Yes")) {
			owner.turn.currentCreature.hasMoved = true;
			reponse = false;
			owner.ChangeState<InterruptUserInputState>();
		}
		if (GUI.Button(new Rect((Screen.width/3)/2,(Screen.height/3)/2,(Screen.width/3)/2,(Screen.height/3)/2),"No")) {
			reponse = false;
		}
		GUI.DragWindow (new Rect (Screen.height/2, Screen.width/2, 10000, 10000));

	}

	void OnGUI() {
		Rect windowValidation = new Rect (Screen.width/2,Screen.height/2,Screen.width/3,Screen.height/3);
		if(reponse) 
			windowValidation = GUI.Window (0, windowValidation, ValidationFunction, "Are you sure ?");

	}

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
			if (tiles.Contains(owner.currentTile))
            {
				reponse = true;           
            }
        }
        else
        {
            owner.ChangeState<SelectUnitState>();
        }
    }




}