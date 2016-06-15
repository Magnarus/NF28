using UnityEngine;
using System.Collections;

public abstract class AgentCreature : Agent {

	private AgentBehaviour attackBehaviour;

	private AgentCreature mInstanceCreator;

	public static AgentCreature Instance
	{
		get
		{
			if (mInstanceCreator == null) mInstanceCreator = new AgentCreature();
			return mInstanceCreator;
		}
	}

	public AgentBehaviour AttackBehaviour {
		get {
			return attackBehaviour;
		}
		set {
			attackBehaviour = value;
		}
	}

	private AgentBehaviour depBehaviour;

	public AgentBehaviour DepBehaviour {
		get {
			return depBehaviour;
		}
		set {
			depBehaviour = value;
		}
	}

	private AgentBehaviour stayBehaviour;

	public AgentBehaviour StayBehaviour {
		get {
			return stayBehaviour;
		}
		set {
			stayBehaviour = value;
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
