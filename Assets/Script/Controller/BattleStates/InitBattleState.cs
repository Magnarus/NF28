using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Descriptors;
using Game;

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
        SpawnTestUnits(creatureJ1, "first"); //Ajout d'une unité pour test
        SpawnTestUnits(creatureJ2, "last");
        yield return null;
        owner.round = owner.gameObject.AddComponent<TurnOrderController>().Round();
        owner.ChangeState<SelectUnitState>(); 

    }

    void SpawnTestUnits(List<Creature> creatureJoueur, string position)
    {
        Point p;
        for (int i = 0; i < 2; ++i)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
            if(position == "first")
            {
                p = new Point((int)levelData.tiles[i].pos.x, (int)levelData.tiles[i].pos.z);
            } else
            {
                p = new Point((int)levelData.tiles[levelData.tiles.Count - 1].pos.x, (int)levelData.tiles[i].pos.z);
            }    
            

            Creature unit = instance.GetComponent<Creature>();
            LoadDefaultStats(unit.gameObject, unit.classCreature);
            unit.Place(board.tiles[p]);
            unit.Match();
            creatureJoueur.Add(unit);

        }
    }

    public void LoadDefaultStats(GameObject c, string type)
    {
        // Différents types de déplacement;
        System.Type[] components = new System.Type[] { typeof(FootMovement) };
        int deplacementType;

        float[] stats;
        switch (type)
        {
            case "warrior": stats = DefaultValues.warriorStats; deplacementType = 0; break;
            case "mage": stats = DefaultValues.mageStats; deplacementType = 0; break;
            default: stats = DefaultValues.archerStats; deplacementType = 0; break;
        }
        CreatureDescriptor d = c.GetComponent<CreatureDescriptor>();
        d.Level.value = (int)stats[0];
        d.HP.value = stats[1];
        d.MP.value = stats[2];
        d.Strength.value = stats[3];
        d.Armor.value = stats[4];
        d.Luck.value = stats[5];


        Movement m = c.AddComponent(components[deplacementType]) as Movement;
        m.range = (int) stats[6];
        m.jumpHeight = (int) stats[7];
    }
       
}