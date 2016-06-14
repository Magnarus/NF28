using UnityEngine;
using System.Collections;

public class AgentBehaviour {

	public abstract bool Run();
	public virtual bool finish() {
		return true;
	}
}
