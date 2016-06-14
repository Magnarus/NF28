using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Descriptors;

public class AgentIATurn : Agent {

	private BattleController controller;
	private List<Creature> creaturesIA;
	private List<CreatureAction> actions = new List<CreatureAction>();
	// Use this for initialization
	void Start () {
		controller = GameObject.Find ("BattleController").GetComponent<BattleController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onRequest(Agent sender, object data){
		Debug.Log ("onRequest IATurn");
		string agentName;
		creaturesIA = controller.creaturesJ2;
		foreach (Creature c in creaturesIA) {
			Agent agent = getCreatureAgent (c.classCreature);
			MessageInfo info = new MessageInfo ("REQUEST", this, null, "choseAction");
			agent.gameObject.SendMessage("MessageReceive",info, SendMessageOptions.DontRequireReceiver);
			}
	}

	public override void onInform (Agent sender, object data) {
		actions.Add ((CreatureAction)data);
		List<CreatureAction> sortedActions = new List<CreatureAction> ();
		//Si on a reçu tous les messages 
		if (actions.Count == creaturesIA.Count) {
			List<CreatureAction> tmpAction = getKillActionList(actions);
			sortedActions.AddRange (tmpAction.GetRange (0, tmpAction.Count - 1));
			tmpAction = getDamageActionList (actions);
			sortedActions.AddRange(tmpAction.GetRange(0, tmpAction.Count -1));
			sortedActions.AddRange(actions.GetRange(0, actions.Count - 1));
				foreach(CreatureAction action in sortedActions) {
					Agent agent = getCreatureAgent (action.Actor.classCreature);
					MessageInfo info = new MessageInfo ("REQUEST", this, action, "doAction");
					agent.gameObject.SendMessage ("ReceiveMessage", info, SendMessageOptions.DontRequireReceiver);
				}
		}
		controller.ChangeState<SelectUnitState>();
		Debug.Log ("onInform IATurn");
	}

	private List<CreatureAction> getKillActionList(List<CreatureAction> actions) {
		List<CreatureAction> killAction = new List<CreatureAction> ();
		foreach (CreatureAction action in actions) {
			if (action.Target.gameObject.GetComponent<CreatureDescriptor> ().HP.CurrentValue - action.Damage <= 0) {
				killAction.Add (action);
				actions.Remove (action);
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
					damageActions.Insert (cpt, action);
				}
				actions.Remove (action);
			}
		}
		return damageActions;
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
