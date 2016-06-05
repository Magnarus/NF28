using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState
{
        int index = -1;
        public string currentPlayer = "J1";

        public override void Enter()
        {
            base.Enter();
            StartCoroutine("ChangeCurrentUnit");
        }

        IEnumerator ChangeCurrentUnit()
        {
            switchPlayerIfNecessary();
            
            index = (index + 1) % creatureJ1.Count;
            if(currentPlayer == "J1")
            {
                 turn.Change(creatureJ1[index]);
            } else {
               turn.Change(creatureJ2[index]);
            }   
            
            yield return null;
            owner.ChangeState<CommandSelectionState>();

        }
        
    // Switch entre player avant mieux peut être
    public void switchPlayerIfNecessary()
    {
        if(index + 1 == creatureJ1.Count)
        {
            currentPlayer = "J2";
        } else
        {
            currentPlayer = "J1";
        }
    }

}