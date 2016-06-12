using UnityEngine;
using System.Collections;

public class PassiveGameState : BaseGameState 
{
	public override void Enter ()
	{
		base.Enter ();
		//GameStateLabel.text = "Opponent's Turn!";
		//RefreshPlayerLabels();
	}
}
