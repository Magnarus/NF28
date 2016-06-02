using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState
{

    /** Le personnage se déplace **/
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }

    /** Le personnage tire **/
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        GameObject content = owner.currentTile.contentTile;
        if (content != null)
        {
            owner.currentUnit = content.GetComponent<Creature>();
            owner.ChangeState<MoveTargetState>();
        }
    }
}