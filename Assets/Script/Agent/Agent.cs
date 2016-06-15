using UnityEngine;
using System.Collections;

public abstract class Agent : MonoBehaviour {

	public BattleController controller;
	private string name;
	// Use this for initialization

	public void Awake() {
		controller = GameObject.Find ("BattleController").GetComponent<BattleController> ();
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void receiveMessage(MessageInfo messageInfo){
		switch (messageInfo.getPerformatif()) {
		case "REQUEST":
			onRequest (messageInfo.getSender (), messageInfo);
			break;
		case "INFORM":
			onInform (messageInfo.getSender (), messageInfo);
			break;
			
		}
	}

	public abstract void onRequest(Agent sender, object data);
	public abstract void onInform(Agent sender, object data);

}
