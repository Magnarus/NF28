using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {
	
	public const string Started = "PlayerController.Start";
	public const string StartedLocal = "PlayerController.StartedLocal";
	public const string Destroyed = "PlayerController.Destroyed";
	public const string CoinToss = "PlayerController.CoinToss";
	public const string ChangePlayer = "PlayerController.ChangePlayer";

	public string playerID;

	public override void OnStartClient ()
	{
		base.OnStartClient ();
		this.PostNotification(Started);
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		this.PostNotification(StartedLocal);
	}

	void OnDestroy ()
	{
		this.PostNotification(Destroyed);
	}

	[Command]
	public void CmdChangeCurrentPlayer() {
		RpcChangeCurrentPlayer ();
	}

	[ClientRpc]
	void RpcChangeCurrentPlayer() {
		this.PostNotification (ChangePlayer);
	}

}
