using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class MoveTargetState : BattleState
{
    List<PhysicTile> tiles;

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

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
            if (tiles.Contains(owner.currentTile))
            {
                owner.turn.currentCreature.hasMoved = true;
                owner.ChangeState<InterruptUserInputState>();
            }
        }
        else
        {
            owner.ChangeState<SelectUnitState>();
        }
    }
}