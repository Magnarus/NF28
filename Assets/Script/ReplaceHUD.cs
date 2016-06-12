using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ReplaceHUD : MonoBehaviour {
	private NetworkManager manager; // 
	private HostData[] hostList; // Hosts ajoutés au serveur master

	const string gameType = "TroopWars";
	const string gameName = "MyGame";

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		manager = GetComponent<NetworkManager> ();
	}
	
	public void AddPlayer() {
		if (!NetworkServer.active) {
				StartServer ();
		}
	}

	public void StartServer() {
		MasterServer.ipAddress = "127.0.0.1";
		Network.InitializeServer (2, 25000, !Network.HavePublicAddress ());
		MasterServer.RegisterHost(gameType, gameName);
	}


	void OnServerInitialized() {
		Debug.Log ("Server initialized");
	}

}
