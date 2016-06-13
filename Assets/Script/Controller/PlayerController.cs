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
	public const string CharacterDied = "PlayerController.CharacterDeath";
	public const string Victory = "PlayerController.Victory";

	public string playerID;

	public override void OnStartClient ()
	{
		base.OnStartClient ();
		DontDestroyOnLoad (this.gameObject);
		this.PostNotification(Started);
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		DontDestroyOnLoad (this.gameObject);
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
	[Command] 
	public void CmdSyncDeath(Point mort) {
		RpcSyncDeath (mort);
	}

	[ClientRpc]
	public void RpcSyncDeath(Point mort) {
		this.PostNotification (CharacterDied, mort);
	}

	[Command]
	public void CmdSyncVictory(string playerV) {
		RpcSyncVictory (playerV);
	}

	[ClientRpc]
	public void RpcSyncVictory(string mort) {
		this.PostNotification (Victory, mort);
	}

	public void Disconnect() {
		if(Network.connections.Length != 0)
			Network.CloseConnection (Network.connections [Network.connections.Length - 1], true);
	}
}
