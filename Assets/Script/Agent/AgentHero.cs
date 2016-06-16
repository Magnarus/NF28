using UnityEngine;
using System.Collections;

public class AgentHero : AgentCreature {
	
	public AgentHero() : base() {
		attackBehaviour = new HeroAttackBehaviour (this);
	}

	private void SendMessage(MessageInfo info){
		CreatureAction action = choseAction (info);
		MessageInfo infoIATurn = new MessageInfo ("INFORM", this, action);
		DictionaryAgent.getAgent ("IATurnAgent").gameObject.SendMessage("receiveMessage",infoIATurn, SendMessageOptions.DontRequireReceiver);
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
		ActionType typeAction = action.Type;
		//Debug.Log ("Appel du doAction du hero avec " + typeAction);
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
		int sizeChemin = depBehaviour.Chemin.Count;
		switch (action.Type) {
		case ActionType.ATK:
			attackBehaviour.RecreatePath ();
			controller.launchCoroutine(CurrentCreature, attackBehaviour.chemin.ToArray());
			controller.turn.doAttack (action.Actor, action.Target);
			CurrentCreature.hasFinished = true;
			break;
		case ActionType.DEP:
			depBehaviour.RecreatePath ();
			controller.launchCoroutine(CurrentCreature, depBehaviour.chemin.ToArray());
			CurrentCreature.hasFinished = true;
			break;
		case ActionType.STAY:
			break;
		default:
			break;
		}


	}

	public override void onRequest(Agent sender, object data) {
		//Debug.Log ("onRequest AgentHero");
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
		Debug.Log ("onInform AgentHero");
	}
}
