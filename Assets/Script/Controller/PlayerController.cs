using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {
	
	public const string Started = "PlayerController.Start";
	public const string StartedLocal = "PlayerController.StartedLocal";
	public const string Destroyed = "PlayerController.Destroyed";
	public const string CoinToss = "PlayerController.CoinToss";
	public const string ChangePlayer = "PlayerController.ChangePlayer";
	public const string LifeChanged = "PlayerController.LifeChange";
	public const string PositionChanged = "PlayerController.PositionChange";

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


	[Command]
	public void CmdSyncDamage(string[] c) {
		RpcSyncDamage(c);
	}

	[ClientRpc]
	public void RpcSyncDamage(string[] c) {
		this.PostNotification(LifeChanged, c);
	}

	[Command] 
	public void CmdSyncPosition(Point[] parcours) {
		RpcSyncPosition (parcours);
	}

	[ClientRpc]
	public void RpcSyncPosition(Point[] parcours) {
		this.PostNotification (PositionChanged, parcours);
	}
}
