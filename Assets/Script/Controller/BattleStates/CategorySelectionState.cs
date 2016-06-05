using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CategorySelectionState : BaseAbilityMenuState
{
    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuTitle = "Action";
            menuOptions = new List<string>(1);
            menuOptions.Add("Attack");
        }

        abilityMenuPanel.Show(menuTitle, menuOptions);
    }

    protected override void Confirm()
    {
        switch (abilityMenuPanel.selection)
        {
            case 0:
                Attack();
                break;
        }
    }

    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    void Attack()
    {
        turn.hasUnitActed = true;
        owner.ChangeState<CommandSelectionState>();
    }

    void SetCategory(int index)
    {
    /*        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();*/
        // Quand y'aura de la magie toussa
    }
}