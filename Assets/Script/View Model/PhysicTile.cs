using UnityEngine;
using System.Collections;

public class PhysicTile : MonoBehaviour {

    public const float stepHeight = 0.25f; // Hauteur d'un niveau d'écart
    public Point pos; // Positions x et y de la case sur la map
    public int height; // Hauteur du cube
    public string type; // Eau / Boue / Standard

    public GameObject contentTile; // Personnage de la case / Autre si on ajoute des coffres


    [HideInInspector]
    public PhysicTile prev; // Case sur laquelle on était précédemment avant celle ci
    [HideInInspector]
    public int distance; // Distance parcouru pour atteindre cette case

    public Vector3 center { get { return new Vector3(pos.x, height * stepHeight, pos.y); } }

    void Match()
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }

    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }

    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }



}
