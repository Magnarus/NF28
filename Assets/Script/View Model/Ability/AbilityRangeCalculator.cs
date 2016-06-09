using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityRangeCalculator : MonoBehaviour
{

    public int horizontal = 2;
    public int vertical = int.MaxValue;
    public virtual bool directionOriented { get { return true; } }
    protected Creature unit { get { return GetComponentInParent<Creature>(); } }

    /** Portée des tiles **/
    public List<PhysicTile> GetTilesInRange(Board board)
    {
        return  board.Search(unit.tile, "range", ExpandSearch);
    }

    /** Critère de validité **/
    bool ExpandSearch(PhysicTile from, PhysicTile to)
    {
        return (from.distance + 1) <= horizontal && Mathf.Abs(to.height - unit.tile.height) <= vertical;
    }
}