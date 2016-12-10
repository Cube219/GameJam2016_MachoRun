using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        Runner runner = gamePlayer.GetComponent<Runner>();

		runner.playerName = lobby.playerName;
		runner.playerColor = lobby.playerColor;

		runner.transform.position = new Vector2(2f, 2f);
    }
}
