using UnityEngine;
using System.Collections;

public class StayBehaviour : AgentBehaviour {

	private Agent mAgent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override bool Run(){
		return true;
	}

	public override bool finish (){
		return true;
	}
}
