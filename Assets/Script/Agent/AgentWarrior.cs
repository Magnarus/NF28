using UnityEngine;
using System.Collections;

public class AgentWarrior : AgentCreature {

	public AgentWarrior() : base() {
		attackBehaviour = new WarriorAttackBehaviour (this); 
	}

	private void SendMessage(MessageInfo info){
		CreatureAction action = choseAction (info);
		MessageInfo infoIATurn = new MessageInfo ("INFORM", this, action);
		DictionaryAgent.getAgent ("IATurnAgent").gameObject.SendMessage("receiveMessage",infoIATurn, SendMessageOptions.DontRequireReceiver);
	}


	public CreatureAction choseAction(MessageInfo info){
		CreatureAction action = AttackBehaviour.Run ();
		if (action == null) {
			action = DepBehaviour.Run ();
			if (action == null) {
				action = StayBehaviour.Run();
			}
		} 
		return action;
	}


	public void doAction(MessageInfo info){
		CreatureAction action = info.getData () as CreatureAction;
		ActionType typeAction = action.Type;
		Debug.Log ("Appel du doAction du warrior avec " + typeAction);
		if (typeAction.Equals (ActionType.ATK)) {
			if (action.Target == null) {
				action = choseAction (info);
			}
		} else if (typeAction.Equals (ActionType.DEP)) {
			if (action.Destination.contentTile!=null) { // récupérer si la tile est disponible 
				action = choseAction (info);
			}
		}
		executeAction (action);
	}

	public void executeAction(CreatureAction action){
		int sizeChemin = depBehaviour.Chemin.Count;
		switch (action.Type) {
		case ActionType.ATK:
			attackBehaviour.RecreatePath ();
			//StartCoroutine(action.Actor.GetComponent<Movement> ().Traverse (action.Destination));
			controller.launchCoroutine(CurrentCreature, attackBehaviour.chemin.ToArray());
			controller.turn.doAttack (action.Actor, action.Target);
			CurrentCreature.hasFinished = true;
			break;
		case ActionType.DEP:
			depBehaviour.RecreatePath ();
			//StartCoroutine(action.Actor.GetComponent<Movement> ().Traverse (action.Destination));
			//controller.launchCoroutine(CurrentCreature, depBehaviour.chemin.ToArray());
			controller.LaunchCoroutine(CurrentCreature, depBehaviour.chemin);
			CurrentCreature.hasFinished = true;
			break;
		case ActionType.STAY:
			break;
		default:
			break;
		}
	}

	public override void onRequest(Agent sender, object data){
		MessageInfo info = data as MessageInfo;
		if(info.getConversationId() != null && info.getConversationId().Equals("choseAction")){
			CurrentCreature = (Creature)info.getData ();
			SendMessage(info);
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
