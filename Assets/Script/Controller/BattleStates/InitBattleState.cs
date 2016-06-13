using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Descriptors;
using Game;

public class InitBattleState : BattleState
{
    private string[] teamComp = new string[] {  "warrior",  "archer", "hero" };//, "archer", "warrior"

    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        board.LoadBoardFromData(levelData);
        SpawnTestUnits(creatureJ1, "first"); //Ajout d'une unité pour test
        SpawnTestUnits(creatureJ2, "last");
        owner.teamSize = creatureJ1.Count;
        yield return null;
		this.AddObserver(OnMatchReady, MatchController.MatchReady);

    }

    /**  Génération d'un certain nombre d'unités sur le terrain **/
    void SpawnTestUnits(List<Creature> creatureJoueur, string position)
    {
        Point p;
        for (int i = 0; i < teamComp.Length; ++i)
        {
            GameObject instance = getObject(teamComp[i]);

            if(position == "first")
            {
                p = new Point((int)levelData.tiles[i].pos.x, (int)levelData.tiles[i].pos.z);

            } 
            else
            {
                p = new Point((int)levelData.tiles[levelData.tiles.Count-1].pos.x+1, (int)levelData.tiles[i].pos.z);

            }    
            
            Creature unit = instance.GetComponent<Creature>();
            LoadDefaultStats(unit.gameObject, unit.classCreature);
            unit.Place(board.tiles[p]);
            unit.Match();
            creatureJoueur.Add(unit);

        }
    }

    void SpawnTestUnitsV2(List<Creature> creatureJoueur, string position)
    {
        Point p;
        for (int i = 0; i < 1; ++i)
        {
            GameObject instance = getObject(teamComp[i]);

            if (position == "first")
            {
                p = new Point((int)levelData.tiles[i].pos.x, (int)levelData.tiles[i].pos.z);

            }
            else
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

    /** Associe à l'unité son type de déplacement **/
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

        Movement m = c.AddComponent(components[deplacementType]) as Movement;
        m.range = (int) stats[6];
        m.jumpHeight = (int) stats[7];
    }

    /** Instantie un GameObject en fonction du type d'unité souhaité **/
    public GameObject getObject(string unitType) {
        GameObject instance = null;

        switch(unitType) {
            case "hero": instance = Instantiate(owner.heroPrefab); break;
            case "archer": instance = Instantiate(owner.archerPrefab); break;
            case "warrior":instance = Instantiate(owner.warriorPrefab); break;
        }
        return instance;
    }

	void OnMatchReady (object sender, object args)
	{
		Point p;
		if (owner.matchController.hostPlayer.isLocalPlayer) {
			owner.matchController.localPlayer.gameObject.SetActive (false);
			p = new Point ((int)levelData.tiles [0].pos.x, (int)levelData.tiles [0].pos.z);
		} else {
			owner.matchController.hostPlayer.gameObject.SetActive (false);
			p = new Point ((int)levelData.tiles [levelData.tiles.Count-1].pos.x + 1, (int)levelData.tiles [0].pos.z);
		}
		SelectTile (p);
		owner.ChangeState<SelectUnitState>();
	}
       
}