using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour {

	public static List<Runner> runners = new List<Runner>();
	public static GameManager m;

	public Image startImg;
	public Sprite[] countImg;
	//public bool noWinner = true;

	
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

	[ServerCallback]
	void Update()
	{
	}

	public void Init()
	{
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
		//countImg[0].GetComponent<CanvasGroup>().DOFade(0.2f, 0);
		startImg.sprite = countImg[0];
		//countImg[0].GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 1);
		//countImg[0].GetComponent<CanvasGroup>().DOFade(1, 1);
		yield return new WaitForSeconds(1.0f);

		//countImg[1].GetComponent<CanvasGroup>().DOFade(0.2f, 0);
		startImg.sprite = countImg[1];
		//countImg[1].GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 1);
		//countImg[1].GetComponent<CanvasGroup>().DOFade(1, 1);
		yield return new WaitForSeconds(1.0f);

		//countImg[2].GetComponent<CanvasGroup>().DOFade(0.2f, 0);
		startImg.sprite = countImg[2];
		//countImg[2].GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 1);
		//countImg[2].GetComponent<CanvasGroup>().DOFade(1, 1);
		yield return new WaitForSeconds(1.0f);

		startImg.sprite = countImg[3];
		//countImg[2].GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 1);
		//countImg[2].GetComponent<CanvasGroup>().DOFade(1, 1);
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

}
