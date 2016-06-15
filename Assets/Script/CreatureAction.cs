using UnityEngine;
using System.Collections;

public class CreatureAction {



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

	public CreatureAction(ActionType type, Creature actor,  PhysicTile dest = null, Creature target = null, float damage = 0) {
		mType = type;
		mActor = actor;
		mDestination = dest;
		mTarget = target;
		mDamage = damage;
	}
}
