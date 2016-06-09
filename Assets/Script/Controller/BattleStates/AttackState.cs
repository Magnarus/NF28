﻿using System;
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

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {

        GameObject content = owner.currentTile.contentTile;
        currentEnnemy = null;

        if (content != null)
        {
            currentEnnemy = content.GetComponent<Creature>();
            if (currentEnnemy != null && turn.getPlayer(currentEnnemy) != turn.getCurrentTeam())
            {
                Directions dir = (turn.currentCreature.tile).GetDirection((currentEnnemy.tile));
                if (dir != turn.currentCreature.dir) {
                    turn.currentCreature.dir = dir;
                    turn.currentCreature.Match();
                }
                // Si c'est un ennemi do it
                doAttack(turn.currentCreature, currentEnnemy);
                turn.currentCreature.hasFinished = true;
                owner.ChangeState<SelectUnitState>();
            }
        }
        

    }

    private void doAttack(Creature currentCreature, Creature currentEnnemy)
    {
        CreatureDescriptor statsCreature = currentCreature.GetComponent<CreatureDescriptor>();
        CreatureDescriptor statsEnnemy = currentEnnemy.GetComponent<CreatureDescriptor>();
       // Animator anim = currentCreature.GetComponent<Animator>();

        if (currentCreature.type == "warrior" || currentCreature.type == "archer") 
        {
           // anim.Play("AttackMelee2");
        }
        float newLife = statsEnnemy.HP.CurrentValue - (20 + statsCreature.Strength.value - statsEnnemy.Armor.value);
        statsEnnemy.HP.CurrentValue = (newLife < 0.0)? 0 : newLife;
       // anim.Play("Idle");
    }

    void ChangeDirection(Point p)
    {
        Directions dir = p.GetDirection();
        if (turn.currentCreature.dir != dir)
        {
            board.NotSelectedColor(tiles);
            turn.currentCreature.dir = dir;
            turn.currentCreature.Match();
            SelectTiles();
        }
    }

    void SelectTiles()
    {
        tiles = ar.GetTilesInRange(board);
        board.SelectedColor(tiles);
    }
}