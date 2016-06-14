using UnityEngine;
using System.Collections;

public abstract class AgentBehaviour {

	private Agent mParent;
	public Agent MParent {
		get { return mParent; }
		set { mParent = value; }
	}

	public abstract bool Run();

	public virtual bool finish() {
		return true;
	}
}
