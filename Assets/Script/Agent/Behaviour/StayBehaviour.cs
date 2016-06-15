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

	public override CreatureAction Run(){
		return null;
	}

	public override bool finish (){
		return true;
	}
}
