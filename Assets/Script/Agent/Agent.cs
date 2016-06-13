using UnityEngine;
using System.Collections;

public abstract class Agent : MonoBehaviour {

	private string name;
	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void receiveMessage(MessageInfo messageInfo){
		switch (messageInfo.getPerformatif()) {
		case "REQUEST":
			onRequest (messageInfo.getSender ());
			break;
		case "INFORM":
			onInform (messageInfo.getSender ());
			break;
			
		}
	}

	public abstract void onRequest(Agent sender);
	public abstract void onInform(Agent sender);

}
