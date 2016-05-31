using UnityEngine;
using System.Collections;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].pos.x, (int)levelData.tiles[0].pos.z);
        SelectTile(p);
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}