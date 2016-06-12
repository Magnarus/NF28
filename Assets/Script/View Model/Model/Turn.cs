using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Descriptors;

public class Turn
{
    // Unité qui doit prendre son tour
    public Creature currentCreature;
    // Liste des créatures du tour
    public List<Creature> turnCreature = new List<Creature>();
    // Est-ce que l'unité a déjà bougé ? 
    public bool hasUnitMoved;
    // Est-ce que l'unité a déjà attaqué ? 
    public bool hasUnitActed;

    // La case d'origine de la créature
    PhysicTile startTile;
    // La direction d'origine
    Directions startDir;
    //
    public BattleController owner;
    public GameObject ability;

    // Change la créature qui doit prendre son tour et réinitilise les champs de gestion du tour
    public void Change(Creature current)
    {
		currentCreature = current;
		if (!turnCreature.Contains (current)) {
			turnCreature.Add (current);
			current.hasMoved = false;
			current.hasFinished = false;
			hasUnitMoved = false;
			hasUnitActed = false;            
		} else {
			hasUnitMoved = current.hasMoved;
			hasUnitActed = current.hasFinished;
		}
		startTile = currentCreature.tile;
		startDir = currentCreature.dir;
       
    }


    // Annule l'action de l'utilisateur, il se replace à la position et dans la direction originelle
    public void UndoMove()
    {
        if(!currentCreature.hasFinished) {
            hasUnitMoved = false;
            currentCreature.Place(startTile);
            currentCreature.dir = startDir;
            currentCreature.Match();
        }
    }

    public bool isTurnOver()
    {
        foreach(Creature c in turnCreature)
        {
            if(c.hasFinished == false)
            {
                return false;
            } 
        }
		return turnCreature.Count == (getCurrentTeam() == "J1"? owner.creaturesJ1.Count : owner.creaturesJ2.Count);
    }

    public void Clear()
    {
        foreach (Creature c in turnCreature)
        {
            c.hasMoved = false;
            c.hasFinished = false;
        }
        turnCreature.Clear();
    }

    public string getCurrentTeam() {

        if (owner.creaturesJ2.Contains(currentCreature)) return "J2";
        return "J1";
    }

    public string getPlayer(Creature c) {
        if (owner.creaturesJ2.Contains(c)) return "J2";
        return "J1";
    }

    /** Implémentation de l'attaque si la saisie est correcte **/
    private float doAttack(Creature currentCreature, Creature currentEnnemy)
    {
        CreatureDescriptor statsCreature = currentCreature.GetComponent<CreatureDescriptor>();
        CreatureDescriptor statsEnnemy = currentEnnemy.GetComponent<CreatureDescriptor>();
        Animator anim = currentCreature.GetComponent<Animator>();

        if (currentCreature.classCreature == "warrior" || currentCreature.classCreature == "hero")
        {
            anim.SetTrigger("AttackM");
        } else if(currentCreature.classCreature == "archer")
        {
            anim.SetTrigger("AttackR");
        }
        float newLife = statsEnnemy.HP.CurrentValue - (20 + statsCreature.Strength.value - statsEnnemy.Armor.value);
		newLife = (newLife > 0) ? newLife : 0;
		Debug.Log ("Life : " + newLife);
		statsEnnemy.HP.CurrentValue = newLife;
		owner.matchController.localPlayer.CmdSyncDamage (new string[] {
																currentEnnemy.tile.pos.x.ToString(),
																currentEnnemy.tile.pos.y.ToString(), 
																newLife.ToString()
														 });
		return newLife;

    }

    public void ManageBattle(Creature ennemy) {
        if (ennemy != null && getPlayer(ennemy) != getCurrentTeam())
        {
            Directions dir = (currentCreature.tile).GetDirection((ennemy.tile));
            if (dir != currentCreature.dir)
            {
                currentCreature.dir = dir;
                currentCreature.Match();
            }
            // Si c'est un ennemi do it
            float newLife = doAttack(currentCreature, ennemy);
            currentCreature.hasFinished = true;
			if(newLife <= 0) {
				killCreature (ennemy);
				owner.ChangeState<VictoryConditionAgent>();
			} else  owner.ChangeState<SelectUnitState>();
        }
    }

	private void killCreature(Creature c) {
		List<Creature> creatureList;
		if (getCurrentTeam () == "J1") {
			creatureList = owner.creaturesJ1;
		} else creatureList = owner.creaturesJ2;
		c.gameObject.GetComponent<Animator> ().SetTrigger ("Die");
		creatureList.Remove (c);
		GameObject.Destroy(c.gameObject, 5.0f);
	}


}