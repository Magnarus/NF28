using UnityEngine;
using System.Collections;

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
        
        turn.hasUnitMoved = true;
        Movement m = turn.currentCreature.GetComponent<Movement>();
        yield return StartCoroutine(m.Traverse(owner.currentTile));
        turn.currentCreature.Match();
        owner.ChangeState<SelectUnitState>();
    }
}