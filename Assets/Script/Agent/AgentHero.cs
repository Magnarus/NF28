using UnityEngine;
using System.Collections;

public class AgentHero : AgentCreature {

	private static AgentHero mAgentHero;

	public static AgentHero Instance
	{
		get
		{
			if (mAgentHero == null) mAgentHero = new AgentHero();
			return mAgentHero;
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
		if (mAgentHero.DepBehaviour.finish ()) {//On se bat !

		} else if (mAgentHero.AttackBehaviour.finish ()) {//On se déplace

		} else {//On reste sur place

		}		

	}


	public void doAction(MessageInfo info){
	}


	public override void onRequest(Agent sender, object data){
		Debug.Log ("onRequest AgentHero");
		MessageInfo info = data as MessageInfo;
		if(info.getConversationId().Equals("choseAction")){
			//choseAction(info);
		}
		else
		{
			//doAction(info);
		}
	}


	public override void onInform(Agent sender, object data){
		Debug.Log ("onInform AgentHero");
	}
}
