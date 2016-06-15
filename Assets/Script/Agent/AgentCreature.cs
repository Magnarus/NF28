using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentCreature : Agent {

	public AgentCreature() : base()
	{
		depBehaviour = new DepBehaviour (this);
		stayBehaviour = new StayBehaviour (this);
	}

	protected AttackBehaviour attackBehaviour;

	public AttackBehaviour AttackBehaviour {
		get {
			return attackBehaviour;
		}
		set {
			attackBehaviour = value;
		}
	}

	protected DepBehaviour depBehaviour;

	public DepBehaviour DepBehaviour {
		get {
			return depBehaviour;
		}
		set {
			depBehaviour = value;
		}
	}

	private StayBehaviour stayBehaviour;

	public StayBehaviour StayBehaviour {
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
		set {
			mCurrentCreature = value;
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
