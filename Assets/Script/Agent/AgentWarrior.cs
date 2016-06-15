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

	private void SendMessage(MessageInfo info){
		CreatureAction action = choseAction (info);
		MessageInfo infoIATurn = new MessageInfo ("INFORM", this, action);
		DictionaryAgent.getAgent ("IATurnAgent").gameObject.SendMessage("MessageReceive",infoIATurn, SendMessageOptions.DontRequireReceiver);
	}


	public CreatureAction choseAction(MessageInfo info){
		CreatureAction action = mAgentWarrior.AttackBehaviour.Run ();
		if (action == null) {
			action = mAgentWarrior.DepBehaviour.Run ();
			if (action == null) {
				action = mAgentWarrior.StayBehaviour.Run();
			}
		} 
		return action;
	}


	public void doAction(MessageInfo info){
		CreatureAction action = info.getData () as CreatureAction;
		string typeAction = action.GetType ().ToString();
		if (typeAction.Equals (ActionType.ATK)) {
			if (action.Target == null) {
				action = choseAction (info);
			}
		}else if (typeAction.Equals (ActionType.DEP)) {
			if (action.Destination.contentTile!=null) { // récupérer si la tile est disponible 
				action = choseAction (info);
			}
		}
		executeAction (action);
	}

	public void executeAction(CreatureAction action){
		switch (action.Type) {
		case ActionType.ATK:
			controller.turn.doAttack (action.Actor, action.Target);
			mAgentWarrior.CurrentCreature.hasFinished = true;
			break;
		case ActionType.DEP:
			action.Actor.GetComponent<Movement> ().Traverse (action.Destination);
			mAgentWarrior.CurrentCreature.hasFinished = true;
			break;
		case ActionType.STAY:
			break;
		default:
			break;
		}
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
