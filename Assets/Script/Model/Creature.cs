using UnityEngine;
using System.Collections;
using Descriptors;

[RequireComponent(typeof(CreatureDescriptor))]
public class Creature : MonoBehaviour
{
    public PhysicTile tile { get; protected set; }
    public Directions dir;
    public string type = "foot";

    public void Place(PhysicTile target)
    {
        // Si l'unité était placé sur une tile (aka le jeu a déjà démarré) on réinitialise la valeur de son contenu
        if (tile != null && tile.contentTile == gameObject)
            tile.contentTile = null;

        // Et mon met à jour la nouvelle position
        tile = target;

        // En finissant par mettre à jour la case cible
        if (target != null)
            target.contentTile = gameObject;
    }

    public void Match()
    {
        // Déplace le personnage pour qu'il soit positionné au milieu de la case
        transform.localPosition = tile.center;
        // Et remet la personnage dans la direction souhaitée
        transform.localEulerAngles = dir.ToEuler();
    }
}