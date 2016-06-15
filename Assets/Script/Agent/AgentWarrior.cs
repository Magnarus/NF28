using UnityEngine;
using System.Collections;

public class AgentWarrior : AgentCreature {

	private static AgentWarrior mAgentWarrior;

	public static AgentWarrior Instance
	{
		get
		{
			if (mAgentWarrior == null) mAgentWarrior = new AgentWarrior();
			return mAgentWarrior;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void choseAction(MessageInfo info){
		CreatureAction action;
		if (mAgentWarrior.AttackBehaviour.finish ()) {//On se bat !
			action = new CreatureAction (ActionType.ATK, null, null, 0, null);
		} else if (mAgentWarrior.DepBehaviour.finish ()) {//On se déplace
			action = new CreatureAction (ActionType.DEP, null, null, 0, null);
		} else {//On reste sur place
			action = new CreatureAction (ActionType.STAY, null, null, 0, null);
		}		

	}


	public void doAction(MessageInfo info){
	}

	public override void onRequest(Agent sender, object data){
		Debug.Log ("onRequest AgentWarrior");
		MessageInfo info = data as MessageInfo;
		if(info.getConversationId().Equals("choseAction")){
			choseAction(info);
		}
		else
		{
			doAction(info);
		}
	}


	public override void onInform(Agent sender, object data){
		Debug.Log ("onInform AgentWarrior");
	}
}
