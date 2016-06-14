using UnityEngine;
using System.Collections;

public abstract class AgentBehaviour {

	public abstract bool Run();
	public virtual bool finish() {
		return true;
	}
}
