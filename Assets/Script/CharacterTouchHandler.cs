using UnityEngine;
using System.Collections;

public class CharacterTouchHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnTouch() {
		Creature creature = gameObject.GetComponent<Creature> ();
		PhysicTile tile = creature.tile;
		BattleController controller = GameObject.Find ("BattleController").GetComponent<BattleController> ();
		controller.tileSelectionIndicator.localPosition = tile.center;
		controller.pos = tile.pos;
		if (controller.CurrentState.GetType () == typeof(SelectUnitState)) {
			((SelectUnitState)controller.CurrentState).characterClicked ();
		} else if (controller.CurrentState.GetType () == typeof(AttackState)) {
			Debug.Log ("I'm in AttackState state");
			((AttackState)controller.CurrentState).Attack ();
		}
	}
}
