using UnityEngine;
using System.Collections;

public class InterruptUserInputState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine("Sequence");
    }

    IEnumerator Sequence()
    {
        turn.hasUnitMoved = true;
        Movement m = turn.currentCreature.GetComponent<Movement>();
        yield return StartCoroutine(m.Traverse(owner.currentTile));
        owner.ChangeState<SelectUnitState>();
    }
}