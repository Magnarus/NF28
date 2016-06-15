using UnityEngine;
using System.Collections;

public class AgentMageArcher : AgentCreature {


	private static AgentMageArcher mAgentMageArcher;

	public static AgentMageArcher Instance
	{
		get
		{
			if (mAgentMageArcher == null) mAgentMageArcher = new AgentMageArcher();
			return mAgentMageArcher;
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
		if (mAgentMageArcher.AttackBehaviour.finish ()) {//On se bat !

		} else if (mAgentMageArcher.DepBehaviour.finish ()) {//On se déplace

		} else {//On reste sur place

		}		

	}


	public void doAction(MessageInfo info){
	}



	public override void onRequest(Agent sender, object data){
		Debug.Log ("onRequest AgentMageArcher");
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
		Debug.Log ("onInform AgentMageArcher");
	}
}
