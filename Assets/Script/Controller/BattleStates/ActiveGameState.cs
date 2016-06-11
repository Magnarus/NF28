using UnityEngine;
using System.Collections;

public class ActiveGameState : BaseGameState 
{
	public override void Enter ()
	{
		base.Enter ();
		GameStateLabel.text = "Your Turn!";
		RefreshPlayerLabels();
	}

	protected override void AddListeners ()
	{
		base.AddListeners ();
		//this.AddObserver(OnBoardSquareClicked, Board.SquareClickedNotification);
	}

	protected override void RemoveListeners ()
	{
		base.RemoveListeners ();
		//this.RemoveObserver(OnBoardSquareClicked, Board.SquareClickedNotification);
	}

	void OnBoardSquareClicked (object sender, object args)
	{
		
	}
}