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

	private void SendMessage(MessageInfo info){
		CreatureAction action = choseAction (info);
		MessageInfo infoIATurn = new MessageInfo ("INFORM", this, action);
		mAgentMageArcher.gameObject.SendMessage("MessageReceive",infoIATurn, SendMessageOptions.DontRequireReceiver);
	}

	public CreatureAction choseAction(MessageInfo info){
		CreatureAction action = mAgentMageArcher.DepBehaviour.Run ();
		if (action == null) {
			action = mAgentMageArcher.AttackBehaviour.Run ();
			if (action == null) {
				action = mAgentMageArcher.StayBehaviour.Run();
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
			mAgentMageArcher.CurrentCreature.hasFinished = true;
			break;
		case ActionType.DEP:
			action.Actor.GetComponent<Movement> ().Traverse (action.Destination);
			mAgentMageArcher.CurrentCreature.hasFinished = true;
			break;
		case ActionType.STAY:
			break;
		default:
			break;
		}


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
