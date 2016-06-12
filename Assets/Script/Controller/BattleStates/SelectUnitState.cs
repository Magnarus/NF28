using UnityEngine;
using System.Collections.Generic;

public class SelectUnitState : BattleState
{
    int index = 0;
    public string currentPlayer = "J1";
    List<Creature> currentPlayerList;
    

    public override void Enter()
    {
		this.AddObserver (OnPlayerSwitched, PlayerController.ChangePlayer);
        base.Enter();
        if(index != 0)    switchPlayerIfNecessary();
        if (!canvas) {
            GameObject parent = GameObject.Find("Canvas");
            foreach(Transform child in parent.transform) {
                  if(child.name == "Left") {
                    canvas = child.gameObject;
                    break;
                }
            }
        }
        canvas.SetActive(false);
    }

	public override void Exit() {
		this.RemoveObserver (OnPlayerSwitched, PlayerController.ChangePlayer);
		base.Exit ();
	}

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
		if(owner.matchController.localPlayer.playerID == currentPlayer) {
			GameObject content = owner.currentTile.contentTile;
			currentPlayerList = (currentPlayer == "J1") ? creatureJ1 : creatureJ2;
			Creature c = null;

			if (content != null)
			{
				c = content.GetComponent<Creature>();
				if (c != null && currentPlayerList.Contains(c) && !c.hasFinished)
				{
					index++;
					owner.turn.Change(c);
					canvas.SetActive(true);
					owner.ChangeState<CategorySelectionState>();
				}
			}
		}
        
    }


    // Switch entre player avant mieux peut être
    public void switchPlayerIfNecessary()
    {
        if(turn.isTurnOver())
        {
			index = 0;
			turn.Clear();
            currentPlayer = (currentPlayer == "J1") ? "J2" : "J1";
			owner.matchController.localPlayer.CmdChangeCurrentPlayer ();
        } 
    }

	public void OnPlayerSwitched(object sender, object args) {
		PlayerController s = (PlayerController)sender;
		if (s.playerID != owner.matchController.localPlayer.playerID) {
			currentPlayer = (currentPlayer == "J1") ? "J2" : "J1";
			turn.currentCreature.hasMoved = false;
			turn.currentCreature.hasFinished = false;
		}

	}
}