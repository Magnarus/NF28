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

	private void SendMessage(MessageInfo info){
		CreatureAction action = choseAction (info);
		MessageInfo infoIATurn = new MessageInfo ("INFORM", this, action);
		mAgentHero.gameObject.SendMessage("MessageReceive",infoIATurn, SendMessageOptions.DontRequireReceiver);
	}

	public CreatureAction choseAction(MessageInfo info){
		CreatureAction action = mAgentHero.DepBehaviour.Run ();
		if (action == null) {
			action = mAgentHero.AttackBehaviour.Run ();
			if (action == null) {
			action = mAgentHero.StayBehaviour.Run();
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
			if (action.Destination.contentTile!=null) {
				action = choseAction (info);
			}
		}
		executeAction (action);
	}

	public void executeAction(CreatureAction action){
		switch (action.Type) {
		case ActionType.ATK:
			controller.turn.doAttack (action.Actor, action.Target);
			mAgentHero.CurrentCreature.hasFinished = true;
			break;
		case ActionType.DEP:
			action.Actor.GetComponent<Movement> ().Traverse (action.Destination);
			mAgentHero.CurrentCreature.hasFinished = true;
			break;
		case ActionType.STAY:
			break;
		default:
			break;
		}


	}

	public override void onRequest(Agent sender, object data){
		Debug.Log ("onRequest AgentHero");
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
		Debug.Log ("onInform AgentHero");
	}
}
