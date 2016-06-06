using UnityEngine;
using System.Collections.Generic;

public class SelectUnitState : BattleState
{
    int index = 0;
    public string currentPlayer = "J1";
    List<Creature> currentPlayerList;

    public override void Enter()
    {
        base.Enter();
        if(index != 0)    switchPlayerIfNecessary();
    }
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        GameObject content = owner.currentTile.contentTile;
        currentPlayerList = (currentPlayer == "J1") ? creatureJ1 : creatureJ2;
        Creature c = null;

        if (content != null)
        {
            c = content.GetComponent<Creature>();
        }
        if (c != null && currentPlayerList.Contains(c) && !c.hasMoved)
        {
            index++;
            owner.turn.Change(c);
            owner.ChangeState<CommandSelectionState>();
        }
    }


    // Switch entre player avant mieux peut être
    public void switchPlayerIfNecessary()
    {
        if(turn.isTurnOver())
        {
            currentPlayer = (currentPlayer == "J1") ? "J2" : "J1";
            index = 0;
            turn.Clear();
        } 
        Debug.Log("turn is over : " + turn.isTurnOver());
        Debug.Log("Current player is : " + currentPlayer);
    }
}