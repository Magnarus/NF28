using UnityEngine;
using System.Collections;

public class AgentIATurn : Agent {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onRequest(Agent sender){
		Debug.Log ("onRequest IATurn");
	}


	public override void onInform(Agent sender){
		Debug.Log ("onInform IATurn");
	}
}
