using UnityEngine;
using System.Collections;

public class CreatureAction {

	public CreatureAction(ActionType _type, Creature _target, PhysicTile _dest, float _damage, Creature _actor){
		Type = _type;
		Target = _target;
		Destination = _dest;
		Damage = _damage;
		Actor = _actor;
	}


	private ActionType mType;
	public ActionType Type {
		get { return mType; }
		set { mType = value; }
	}

	private Creature mTarget;
	public Creature Target {
		get { return mTarget; }
		set { mTarget = value; }
	}

	private PhysicTile mDestination;
	public PhysicTile Destination {
		get { return mDestination; }
		set { mDestination = value; }
	}

	private float mDamage;
	public float Damage {
		get { return mDamage; }
		set { mDamage = value; }
	}

	private Creature mActor;
	public Creature Actor {
		get {
			return mActor;
		}
		set {
			mActor = value;
		}
	}
}
