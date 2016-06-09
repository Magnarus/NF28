using System;
using System.Collections;
using System.Collections.Generic;
using Descriptors;
using UnityEngine;

public class AttackState : BattleState
{
    List<PhysicTile> tiles;
    AbilityRangeCalculator ar;
    Creature currentEnnemy;

    public override void Enter()
    {
        base.Enter();
        ar = turn.ability.GetComponent<AbilityRangeCalculator>();
        SelectTiles();
    }

    public override void Exit()
    {
        base.Exit();
        board.NotSelectedColor(tiles);
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
            SelectTile(e.info + pos);
    }

    /** Appelé à la selection d'un tile pour attaque, on vérifie si on peut y faire quelque chose **/
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {

        GameObject content = owner.currentTile.contentTile;
        currentEnnemy = null;

        if (content != null)
        {
            currentEnnemy = content.GetComponent<Creature>();
            turn.ManageBattle(currentEnnemy);
            
        }
        

    }

   
    /** Active l'overlay de sélection de la cible **/
    void SelectTiles()
    {
        tiles = ar.GetTilesInRange(board);
        board.SelectedColor(tiles);
    }
}