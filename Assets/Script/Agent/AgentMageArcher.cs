using UnityEngine;
using System.Collections;

public class AgentMageArcher : AgentCreature {

	protected override void OnStart() {
		base.OnStart ();
		attackBehaviour = new ArcherAttackBehaviour (this);
	}

	private void SendMessage(MessageInfo info){
		CreatureAction action = choseAction (info);
		MessageInfo infoIATurn = new MessageInfo ("INFORM", this, action);
		DictionaryAgent.getAgent ("IATurnAgent").gameObject.SendMessage("MessageReceive",infoIATurn, SendMessageOptions.DontRequireReceiver);
	}

	public CreatureAction choseAction(MessageInfo info){
		CreatureAction action = DepBehaviour.Run ();
		if (action == null) {
			action = AttackBehaviour.Run ();
			if (action == null) {
				action = StayBehaviour.Run();
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
			CurrentCreature.hasFinished = true;
			break;
		case ActionType.DEP:
			action.Actor.GetComponent<Movement> ().Traverse (action.Destination);
			CurrentCreature.hasFinished = true;
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
