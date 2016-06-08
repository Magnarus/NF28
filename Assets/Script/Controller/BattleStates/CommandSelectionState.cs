using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSelectionState : BaseAbilityMenuState
{
    /** Load le menu des capacités, il y aura toujours les trois mêmes donc pas besoin de faire quelque de plus élaboré **/
    protected override void LoadMenu()
    {
        //On se déplace à la position
        //Point p = owner.currentTile.pos;
        //SelectTile(p);

        List<string> s = new List<string>();
        s.Add("Déplacement");
        s.Add("Attaque");
        s.Add("Attendre");
    }

    protected override void Confirm()
    {
    }

    /** Si l'unité a déjà bougé et qu'on annule, retour case départ **/
    protected override void Cancel()
    {
        if (turn.hasUnitMoved)
        {
            turn.UndoMove();
            //On se déplace à la position
            Point p = owner.currentTile.pos;
            SelectTile(p);
        }
        else
        {
            owner.ChangeState<ExploreState>();
        }
    }

}