using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turn
{
    // Unité qui doit prendre son tour
    public Creature currentCreature;
    // Est-ce que l'unité a déjà bougé ? 
    public bool hasUnitMoved;
    // Est-ce que l'unité a déjà attaqué ? 
    public bool hasUnitActed;
    // La case d'origine de la créature
    PhysicTile startTile;
    // La direction d'origine
    Directions startDir;


    // Change la créature qui doit prendre son tour et réinitilise les champs de gestion du tour
    public void Change(Creature current)
    {
        currentCreature = current;
        hasUnitMoved = false;
        hasUnitActed = false;
        startTile = currentCreature.tile;
        startDir = currentCreature.dir;
    }


    // Annule l'action de l'utilisateur, il se replace à la position et dans la direction originelle
    public void UndoMove()
    {
        hasUnitMoved = false;
        currentCreature.Place(startTile);
        currentCreature.dir = startDir;
        currentCreature.Match();
    }
}