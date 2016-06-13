using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour {


	[SerializeField] private Button btn = null;

	// Use this for initialization
	void Start () {
		btn.onClick.AddListener (() => { ExitGame(); });
	}
	
	public void ExitGame() {
		/*

		if (!bc.matchController.localPlayer.gameObject) {
			Destroy (bc.matchController.localPlayer.gameObject);
			Destroy (bc.matchController.remotePlayer.gameObject);
		}
		Destroy (GameObject.Find("BattleController"));
		Destroy (GameObject.Find("Match Controller"));
		Destroy (GameObject.Find("DiscoveryManager"));*/
		BattleController bc = GameObject.Find ("BattleController").GetComponent<BattleController> ();
		MyNetworkManager m = GameObject.Find ("NetworkManager").GetComponent<MyNetworkManager> ();
		if(m.discovery != null) m.StopClient ();
		if(bc.matchController.clientPlayer != null)		bc.matchController.clientPlayer.Disconnect ();
		if(bc.matchController.hostPlayer != null)	 bc.matchController.hostPlayer.Disconnect ();


		GameObject[] allGO  = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject g in allGO) {
			Destroy (g);
		}
		SceneManager.LoadScene ("Menu_NF28");
	}
}
