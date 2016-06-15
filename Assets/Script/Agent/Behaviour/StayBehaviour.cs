using UnityEngine;
using System.Collections;

public class StayBehaviour : AgentBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public override CreatureAction Run(){
		((AgentCreature)Parent).CurrentCreature.hasMoved = true;
		((AgentCreature)Parent).CurrentCreature.hasFinished = true;
		return new CreatureAction(ActionType.STAY, ((AgentCreature)Parent).CurrentCreature);
	}

	public override bool finish (){
		return true;
	}
}
