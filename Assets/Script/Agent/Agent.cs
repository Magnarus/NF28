using UnityEngine;
using System.Collections;

public abstract class Agent : MonoBehaviour {

	public BattleController controller;
	private string name;
	// Use this for initialization

	void Start () {
		OnStart();
	}

	protected virtual void OnStart() {
		controller = GameObject.Find ("BattleController").GetComponent<BattleController> ();
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void receiveMessage(MessageInfo messageInfo){
		switch (messageInfo.getPerformatif()) {
		case "REQUEST":
			onRequest (messageInfo.getSender (), messageInfo.getData());
			break;
		case "INFORM":
			onInform (messageInfo.getSender (), messageInfo.getData());
			break;
			
		}
	}

	public abstract void onRequest(Agent sender, object data);
	public abstract void onInform(Agent sender, object data);

}
