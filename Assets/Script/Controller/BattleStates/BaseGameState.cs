using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseGameState : State {

	public BattleController owner;

	public Board Board { get { return owner.board; }}
	public Text LocalPlayerLabel { get { return owner.localPlayerLabel; }}
	public Text RemotePlayerLabel { get { return owner.remotePlayerLabel; }}
	public Text GameStateLabel { get { return owner.gameStateLabel; }}
	public PlayerController LocalPlayer { get { return owner.matchController.localPlayer; }}
	public PlayerController RemotePlayer { get { return owner.matchController.remotePlayer; }}

	protected virtual void Awake ()
	{
		owner = GetComponent<BattleController>();
	}

	protected void RefreshPlayerLabels ()
	{
	}
		
}
