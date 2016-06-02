using UnityEngine;
using System.Collections;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        board.LoadBoardFromData(levelData);
        Point p = new Point((int)levelData.tiles[0].pos.x, (int)levelData.tiles[0].pos.z);
        SelectTile(p);
        SpawnTestUnits(); // This is new
        yield return null;
        owner.ChangeState<SelectUnitState>(); // This is changed    
    }

    void SpawnTestUnits()
    {
        System.Type[] components = new System.Type[] { typeof(FootMovement)};
        for (int i = 0; i < 1; ++i)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;

            Point p = new Point((int)levelData.tiles[i].pos.x, (int)levelData.tiles[i].pos.z);

            Creature unit = instance.GetComponent<Creature>();
            unit.Place(board.tiles[p]);
            unit.Match();

            Movement m = instance.AddComponent(components[i]) as Movement;
            m.range = 5;
            m.jumpHeight = 1;
        }
    }
}