using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Descriptors;

public class AgentIATurn : Agent {


	private List<Creature> creaturesIA;
	private List<CreatureAction> actions = new List<CreatureAction>();

	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onRequest(Agent sender, object data){
		string agentName;
		creaturesIA = controller.creaturesJ2;
		foreach (Creature c in creaturesIA) {
			Agent agent = getCreatureAgent (c.classCreature);
			MessageInfo info = new MessageInfo ("REQUEST", this, c, "choseAction");
			agent.gameObject.SendMessage("receiveMessage", info, SendMessageOptions.DontRequireReceiver);
		}
	}

	public override void onInform (Agent sender, object data) {
		MessageInfo receivedInfo = (MessageInfo)data;

		actions.Add ((CreatureAction)receivedInfo.getData());
		List<CreatureAction> sortedActions = new List<CreatureAction> ();
		//Si on a reçu tous les messages 
		if (actions.Count == creaturesIA.Count) {
			List<CreatureAction> tmpAction = getKillActionList(actions);
			sortedActions.AddRange (tmpAction);
			tmpAction = getDamageActionList (actions);
			sortedActions.AddRange(tmpAction);
			sortedActions.AddRange(getNotAttackAction(actions));
			foreach(CreatureAction action in sortedActions) {
				Agent agent = getCreatureAgent (action.Actor.classCreature);
				MessageInfo info = new MessageInfo ("REQUEST", this, action, "doAction");
				agent.gameObject.SendMessage ("receiveMessage", info, SendMessageOptions.DontRequireReceiver);
			}
			actions.Clear ();
			controller.ChangeState<SelectUnitState>();
		}


		Debug.Log ("onInform IATurn");
	}

	private List<CreatureAction> getKillActionList(List<CreatureAction> actions) {
		List<CreatureAction> killAction = new List<CreatureAction> ();
		foreach (CreatureAction action in actions) {
			if (action.Target != null && action.Target.gameObject.GetComponent<CreatureDescriptor> ().HP.CurrentValue - action.Damage <= 0) {
				killAction.Add (action);
				//actions.Remove (action);
			}
		}
		return killAction;
	}

	private List<CreatureAction> getDamageActionList(List<CreatureAction> actions) {
		List<CreatureAction> damageActions = new List<CreatureAction> ();
		foreach (CreatureAction action in actions) {
			if (action.Type == ActionType.ATK) {
				if (damageActions.Count == 0) {
					damageActions.Add (action);
				} else {
					int cpt = 0;
					while (damageActions [cpt].Damage > action.Damage && cpt < damageActions.Count) {
						cpt++;
					}
					if(action.Target != null && action.Target.gameObject.GetComponent<CreatureDescriptor> ().HP.CurrentValue - action.Damage > 0)
						damageActions.Insert (cpt, action);
				}
				//actions.Remove (action);
			}
		}
		return damageActions;
	}

	private List<CreatureAction> getNotAttackAction(List<CreatureAction> actions) {
		List<CreatureAction> notAttack = new List<CreatureAction> ();
		foreach (CreatureAction action in actions) {
			if (action.Type != ActionType.ATK)
				notAttack.Add (action);
		}
		return notAttack;

	}

	private Agent getCreatureAgent(string creatureClass) {
		Agent receiver = null;
		string agentName;
		switch (creatureClass) { 
			case "warrior":
				agentName = "warriorAgent";
				break;
			case "archer":
				agentName = "archerAgent";
				break;
			case "mage":
				agentName = "mageAgent";
				break;
			case "hero":
				agentName = "heroAgent";
				break;
			default:
				agentName = null;
				break;
		}
		if (agentName != null) {
			receiver = DictionaryAgent.getAgent (agentName);
		}
		return receiver;
	}
}
