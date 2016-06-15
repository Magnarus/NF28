using UnityEngine;
using System.Collections;

public abstract class AgentCreature : Agent {

	private AgentBehaviour attackBehaviour;

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

}
