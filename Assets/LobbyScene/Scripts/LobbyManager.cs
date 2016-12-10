using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LobbyManager : NetworkLobbyManager {

	public static LobbyManager m;

	void Awake()
	{
		m = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
