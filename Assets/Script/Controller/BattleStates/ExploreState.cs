using UnityEngine;
using System.Collections;


/** Permet de se déplacer pour évaluer ses futurs actions, aucun personnage n'est verrouillé dans cet état**/
public class ExploreState : BattleState
{
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
            owner.ChangeState<CommandSelectionState>();
    }
}