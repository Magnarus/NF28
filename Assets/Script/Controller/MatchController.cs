using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MatchController : MonoBehaviour {

	public const string MatchReady = "MatchController.Ready";

	public bool IsReady { get { return localPlayer != null && remotePlayer != null; }}
	public PlayerController localPlayer;
	public PlayerController remotePlayer;
	public PlayerController hostPlayer;
	public PlayerController clientPlayer;
	public List<PlayerController> players = new List<PlayerController>();

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}

	void OnEnable ()
	{
		this.AddObserver(OnPlayerStarted, PlayerController.Started);
		this.AddObserver(OnPlayerStartedLocal, PlayerController.StartedLocal);
		this.AddObserver(OnPlayerDestroyed, PlayerController.Destroyed);
	}

	void OnDisable ()
	{
		this.RemoveObserver(OnPlayerStarted, PlayerController.Started);
		this.RemoveObserver(OnPlayerStartedLocal, PlayerController.StartedLocal);
		this.RemoveObserver(OnPlayerDestroyed, PlayerController.Destroyed);
	}

	void OnPlayerStarted(object sender, object args) {
		//Debug.Log ("Called");
		players.Add ((PlayerController)sender);
		Configure ();
	}

	void OnPlayerStartedLocal(object sender, object args) {
		//Debug.Log ("Called 2");
		localPlayer = (PlayerController)sender;
		Configure ();
	}
	
	void OnPlayerDestroyed (object sender, object args)
	{
		PlayerController pc = (PlayerController)sender;
		if (localPlayer == pc)
			localPlayer = null;
		if (remotePlayer == pc)
			remotePlayer = null;
		if (hostPlayer == pc)
			hostPlayer = null;
		if (clientPlayer == pc)
			clientPlayer = null;
		if (players.Contains(pc))
			players.Remove(pc);
	}
	
	void Configure ()
	{
		if (localPlayer == null || players.Count < 2)
			return;
	 
		for (int i = 0; i < players.Count; ++i)
		{
			if (players[i] != localPlayer)
			{
				remotePlayer = players[i];
				break;
			}
		}
	 
		hostPlayer = (localPlayer.isServer) ? localPlayer : remotePlayer;
		clientPlayer = (localPlayer.isServer) ? remotePlayer : localPlayer;
		localPlayer.playerID = (localPlayer.isServer) ? "J1" : "J2";
		remotePlayer.playerID = (localPlayer.isServer) ? "J2" : "J1";
	 
		this.PostNotification(MatchReady);
	}

	void OnLevelWasLoaded(int level) {
		if (level != 1) {
			Destroy (this.gameObject);
		}
	}


}
