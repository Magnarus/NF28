using UnityEngine;
using System.Collections;

public abstract class AgentBehaviour {

	private Agent mParent;
	public Agent Parent {
		get { return mParent; }
		set { mParent = value; }
	}

	public abstract CreatureAction Run();

	public virtual bool finish() {
		return true;
	}
}
