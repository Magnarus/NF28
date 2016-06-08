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

    }

    protected override void Confirm()
    {
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
    }
}