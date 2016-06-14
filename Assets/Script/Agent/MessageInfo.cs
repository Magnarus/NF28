using UnityEngine;
using System.Collections;

public class MessageInfo : MonoBehaviour {
	string performatif;
	Agent sender;
	object data;
	string conversationID;
		
	public MessageInfo(string perf, Agent send = null, object data = null, string conversationID = null) {
		performatif = perf;
		sender = send;
		this.data = data;
		this.conversationID = conversationID;
	}

	public string getPerformatif(){
		return performatif;
	}

	public void setPerformatif(string perf){
		this.performatif = perf;
	}

	public Agent getSender(){
		return this.sender;
	}

	public object getData() {
		return data;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
