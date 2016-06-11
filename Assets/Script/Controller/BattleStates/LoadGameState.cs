using UnityEngine;
using System.Collections;

public class LoadGameState : BaseGameState 
{
	public override void Enter ()
	{
		base.Enter ();
		GameStateLabel.text = "Waiting For Players";
		LocalPlayerLabel.text = "";
		RemotePlayerLabel.text = "";
	}

	public override void Exit ()
	{
		base.Exit ();
		RefreshPlayerLabels();
	}
}