using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSelectionState : BaseAbilityMenuState
{
    /** Load le menu des capacités, il y aura toujours les trois mêmes donc pas besoin de faire quelque de plus élaboré **/
    protected override void LoadMenu()
    {   
        //On se déplace à la position
        Point p = owner.currentTile.pos;
        SelectTile(p);

        List<string> s = new List<string>();
        s.Add("Déplacement");
        s.Add("Attaque");
        s.Add("Attendre");
        abilityMenuPanel.Show("Commandes", s);
        abilityMenuPanel.SetLocked(0, owner.turn.hasUnitMoved); // Lock le bouton de déplacement si on a déjà bougé
        abilityMenuPanel.SetLocked(1, owner.turn.hasUnitActed); // Lock le bouton d'action si on a déjà effectué une action
        abilityMenuPanel.Hide(); // Affiche le panneau d'interaction
    }

    protected override void Confirm()
    {
        switch (abilityMenuPanel.selection)
        {
            case 0: // Move
                owner.ChangeState<MoveTargetState>();
                break;
            case 1: // Action
                owner.ChangeState<CategorySelectionState>();
                break;
            case 2: // Wait
                owner.ChangeState<SelectUnitState>();
                break;
        }
    }

    /** Si l'unité a déjà bougé et qu'on annule, retour case départ **/
    protected override void Cancel()
    {
        if (turn.hasUnitMoved)
        {
            turn.UndoMove();
            abilityMenuPanel.SetLocked(0, false);
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