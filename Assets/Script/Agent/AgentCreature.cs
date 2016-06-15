using UnityEngine;
using System.Collections;

public class AgentCreature : Agent {

	private static AgentCreature mInstanceCreator;

	public static AgentCreature Instance
	{
		get
		{
			if (mInstanceCreator == null) mInstanceCreator = new AgentCreature();
			return mInstanceCreator;
		}
	}

	private Creature mCurrentCreature;

	public Creature CurrentCreature {
		get {
			return mCurrentCreature;
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onRequest(Agent sender, object data){
		mCurrentCreature = (Creature) data;
		Debug.Log ("onRequest AgentCreature");
	}


	public override void onInform(Agent sender, object data){
		Debug.Log ("onInform AgentCreature");
	}

}
