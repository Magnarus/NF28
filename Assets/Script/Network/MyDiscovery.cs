using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyDiscovery : NetworkDiscovery  {
	public List<DiscoveredGame> discoveredGames = new List<DiscoveredGame>();

	void Start()
	{
		Initialize();
		StartAsClient();
		StartCoroutine(CheckGamesList());
	}

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		base.OnReceivedBroadcast (fromAddress, data);
		var parts = fromAddress.Split(new char[]{':'});

		bool found = false;
		foreach(var dGame in discoveredGames)
		{
			if(dGame.networkAddress == parts[3])
			{
				found = true;
				dGame.lastSeen = Time.time;
				break;
			}
		}

		if(!found)
		{
			var dGame = new DiscoveredGame();
			dGame.networkAddress = parts[3];
			dGame.networkPort = int.Parse(data);
			dGame.lastSeen = Time.time;
			discoveredGames.Add(dGame);
		}

	}

	IEnumerator CheckGamesList()
	{
		while(true)
		{
			for(int i = discoveredGames.Count -1; i >= 0; i--)
			{
				if(discoveredGames[i].lastSeen < Time.time-1.5f)
				{
					discoveredGames.RemoveAt(i);
				}
			}

			yield return new WaitForSeconds(1.5f);
		}
	}
}

[System.Serializable]
public class DiscoveredGame
{
	public string networkAddress;
	public int networkPort;
	public float lastSeen;
}