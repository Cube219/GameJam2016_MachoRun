using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour {

	public static List<Runner> runners = new List<Runner>();
	public static GameManager m;
	public static SoundPlayer soundPlayer = new SoundPlayer();

	public Image startImg;
	public Sprite[] countImg;
	//public bool noWinner = true;

	private bool isEnd = false;

	
	void Awake()
	{
		m = this;
	}

	
	void Start () {
		ReadyGame();

		MapCreator.m.LoadMapData("test2");
		MapCreator.m.CreateMap();

		foreach(Runner r in runners) {
		//	r.Init();
		}
	}

	private void ReadyGame()
	{
		//if(isServer) {
			StartCoroutine(Ready_c());
		//}
	}
	private IEnumerator Ready_c()
	{
		startImg.gameObject.SetActive(true);
		startImg.sprite = countImg[0];
		yield return new WaitForSeconds(1.0f);
		startImg.sprite = countImg[1];
		yield return new WaitForSeconds(1.0f);
		startImg.sprite = countImg[2];
		yield return new WaitForSeconds(1.0f);
		startImg.sprite = countImg[3];
		yield return new WaitForSeconds(0.5f);
		startImg.gameObject.SetActive(false);

		StartGame();
	}

	private void StartGame()
	{
		foreach(Runner r in runners) {
			r.canRun = true;
		}
	}

	[Server]
	public void Victory(Runner r)
	{
		if(isEnd == false) {
			isEnd = true;
			foreach(Runner run in runners) {
				run.canRun = false;
				run.isBumped = true;
			}
			RpcVictory(r.playerName, r.playerColor);

			StartCoroutine(Victory_c());
		}
	}
	private IEnumerator Victory_c()
	{
		yield return new WaitForSeconds(4f);
		Prototype.NetworkLobby.LobbyManager.s_Singleton.ServerReturnToLobby();
	}

	public Text winText;
	public GameObject eff;

	[ClientRpc]
	public void RpcVictory(string playerName, Color playerColor)
	{
		Debug.Log(playerName + "Win!!");
		winText.text = playerName + " Win !!!";
		eff.SetActive (true);

	}

}
