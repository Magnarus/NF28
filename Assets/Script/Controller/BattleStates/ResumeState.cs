using UnityEngine;
using System.Collections;

public class ResumeState : BattleState
{

    public override void Enter()
    {
        base.Enter();
        //TODO : POST Traitement
        Debug.Log("Someone win");
    }
}