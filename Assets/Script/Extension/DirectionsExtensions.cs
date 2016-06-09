using UnityEngine;
using System.Collections;

public static class DirectionsExtensions
{

    // Fonction qui compare la position de deux cases du board pour retourner la direction dans laquelle se déplacer
    public static Directions GetDirection(this PhysicTile t1, PhysicTile t2)
    {
        if (t1.pos.y < t2.pos.y)
            return Directions.North;
        if (t1.pos.x < t2.pos.x)
            return Directions.East;
        if (t1.pos.y > t2.pos.y)
            return Directions.South;
        return Directions.West;
    }

    public static Vector3 ToEuler(this Directions d)
    {
        return new Vector3(0, (int)d * 90, 0);
    }
    // Retourne la direction en fonction d'un point
    public static Directions GetDirection(this Point p)
    {
        if (p.y > 0)
            return Directions.North;
        if (p.x > 0)
            return Directions.East;
        if (p.y < 0)
            return Directions.South;
        return Directions.West;
    }
}