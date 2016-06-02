using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FootMovement : Movement
{


    /** Fonction de recherche spécifique aux personnages à pieds **/
    protected override bool ExpandSearch(PhysicTile from, PhysicTile to)
    {
        // On ne peut pas sauter plus qu'un certaine hauteur en une fois
        if ((Mathf.Abs(from.height - to.height) > jumpHeight))
            return false;

        // La case n'est pas vide on ne s'y place pas
        if (to.contentTile != null)
            return false;

        return base.ExpandSearch(from, to);
    }

    /** Implémentation du déplacement du personange **/
    public override IEnumerator Traverse(PhysicTile tile)
    {
        // On met à jour les attributs du personnage
        unit.Place(tile);

        // A la façon du liste chainée, en partant de la cible, on récupère le chemin 
        List<PhysicTile> targets = new List<PhysicTile>();
        while (tile != null)
        {
            targets.Insert(0, tile);
            tile = tile.prev;
        }

        // On se déplace de case en case...
        for (int i = 1; i < targets.Count; ++i)
        {
            PhysicTile from = targets[i - 1];
            PhysicTile to = targets[i];

            // En mettant à jour la direction à chaque fois 
            Directions dir = from.GetDirection(to);
            if (unit.dir != dir)
                yield return StartCoroutine(Turn(dir));

            // On vérifie que le mouvement ne s'accompagne pas d'un saut
            if (from.height == to.height)
                yield return StartCoroutine(Walk(to));
            else
                yield return StartCoroutine(Jump(to));
        }

        yield return null;
    }

    /** Déplacement en marchant **/
    IEnumerator Walk(PhysicTile target)
    {
        Tweener tweener = transform.MoveTo(target.center, 0.5f, EasingEquations.Linear);
        while (tweener != null)
            yield return null;
    }

    /** Déplacement en sautant **/
    IEnumerator Jump(PhysicTile to)
    {
        Tweener tweener = transform.MoveTo(to.center, 0.5f, EasingEquations.Linear);

        Tweener t2 = jumper.MoveToLocal(new Vector3(0, PhysicTile.stepHeight * 2f, 0), tweener.easingControl.duration / 2f, EasingEquations.EaseOutQuad);
        t2.easingControl.loopCount = 1;
        t2.easingControl.loopType = EasingControl.LoopType.PingPong;

        while (tweener != null)
            yield return null;
    }
}