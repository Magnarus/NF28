using UnityEngine;
using System.Collections;

public class MessageInfo : MonoBehaviour {
	string performatif;
	Agent sender;

	public MessageInfo(string perf){
		performatif = perf;
	}

	public string getPerformatif(){
		return performatif;
	}

	public void setPerformatif(string perf){
		this.performatif = perf;
	}

	public Agent getSender(){
		return this.sender;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
