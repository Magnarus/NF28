﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MyNetworkManager : NetworkManager
{
	public MyDiscovery discovery;
	[HideInInspector] public bool isHost;

	void Start()
	{

	}

	public void StartGame() {
		if (discovery.discoveredGames.Count == 0) {
			// Create a host
			StartGameHost();
		} else {
			// Create a client
			Debug.Log("J'ai trouvé un host");
			StartGameClient();
		}
	}

	public void StartGameHost()
	{
		isHost = true;
		StartHost();

	}

	public void StartGameClient()
	{
		isHost = false;
		this.networkAddress = discovery.discoveredGames [0].networkAddress;
		StartClient();
		SceneManager.LoadScene (1);

	}

	public override void OnStartHost()
	{
		discovery.StopBroadcast();
		Debug.Log("Start Host Broadcast....");
		SceneManager.LoadScene (1);
		discovery.broadcastData = networkPort.ToString();
		discovery.StartAsServer();

	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer (conn, playerControllerId);
		if(conn.connectionId > 0)
		{
			Debug.Log("Stop Host Broadcast...");
			//discovery.StopBroadcast();
		}
	}

	public override void OnStartClient(NetworkClient client)
	{
		base.OnStartClient(client);
	}

	public override void OnStopClient()
	{
		discovery.StopBroadcast();
	}

	void OnLevelWasLoaded(int level) {
		if (level != 1) {
			Destroy (this.gameObject);
		}
	}
}