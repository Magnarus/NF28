using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Movement : MonoBehaviour
{
    public int range; // Nombre de cases qu'on peut traverser 
    public int jumpHeight; // Hauteur qu'on peut monter/descendre en une fois
    protected Creature unit; // Unité donc on s'occupe du mouvement
    protected Transform jumper;

    /** Assignation des paramètres quand unity trigger le script **/
    protected virtual void Awake()
    {
        unit = GetComponent<Creature>();
        jumper = transform.FindChild("Jumper");
    }

    /** Récupère les tiles disponibles pour des criètes donnés **/
    public virtual List<PhysicTile> GetTilesInRange(Board board)
    {
		//Debug.Log ("Unit : " + unit.classCreature);
        List<PhysicTile> retValue = board.Search(unit.tile, unit.type, ExpandSearch);
        Filter(retValue);
        return retValue;
    }
    
    /** Méthode utilisée par le board pour savoir si une case est OK pour une unité donnée **/
    protected virtual bool ExpandSearch(PhysicTile from, PhysicTile to)
    {
        return (from.distance + 1) <= range;
    }

    /** On ne peut pas se positionner sur une case occupée **/
    protected virtual void Filter(List<PhysicTile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            if (tiles[i].contentTile != null)
                tiles.RemoveAt(i);
    }

    /** Classe abstraite pour les animations de déplacement **/
    public abstract IEnumerator Traverse(PhysicTile tile);


    /** Fonction qui permet de tourner le personnage dans la bonne direction. Merci google  **/
    protected virtual IEnumerator Turn(Directions dir)
    {
        TransformLocalEulerTweener t = (TransformLocalEulerTweener)transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);

        // Calcul de la nouvelle direction / transformation
        if (Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
            t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        else if (Mathf.Approximately(t.startValue.y, 270) && Mathf.Approximately(t.endValue.y, 0))
            t.endValue = new Vector3(t.startValue.x, 360f, t.startValue.z);

        // Assignation de la direction
        unit.dir = dir;

        while (t != null)
            yield return null;
    }
}