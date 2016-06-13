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
		BattleController bc = GameObject.Find ("BattleController").GetComponent<BattleController> ();
		MyNetworkManager m = GameObject.Find ("NetworkManager").GetComponent<MyNetworkManager> ();
		if (bc.matchController.localPlayer.isServer) {
			Debug.Log ("Je suis l'host et je suis déco!");
			MyNetworkManager.singleton.StopHost ();
		} else {
			MyNetworkManager.singleton.StopClient ();
		}
		Debug.Log ("Client connected : " + m.IsClientConnected() + " network actif : " + m.isNetworkActive);

		GameObject[] allGO  = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject g in allGO) {
			Destroy (g);
		}
		SceneManager.LoadScene ("Menu_NF28");
	}
}
