using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

	public Image[] countImg;

	// Use this for initialization
	void Start () {
		ReadyGame();
	}

	private void Init()
	{
	}

	private void ReadyGame()
	{
		StartCoroutine(Ready_c());
	}
	private IEnumerator Ready_c()
	{
		countImg[0].GetComponent<CanvasGroup>().DOFade(0.2f, 0);
		countImg[0].gameObject.SetActive(true);
		countImg[0].GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 1);
		countImg[0].GetComponent<CanvasGroup>().DOFade(1, 1);
		yield return new WaitForSeconds(1.0f);
		countImg[0].gameObject.SetActive(false);

		countImg[1].GetComponent<CanvasGroup>().DOFade(0.2f, 0);
		countImg[1].gameObject.SetActive(true);
		countImg[1].GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 1);
		countImg[1].GetComponent<CanvasGroup>().DOFade(1, 1);
		yield return new WaitForSeconds(1.0f);
		countImg[1].gameObject.SetActive(false);

		countImg[2].GetComponent<CanvasGroup>().DOFade(0.2f, 0);
		countImg[2].gameObject.SetActive(true);
		countImg[2].GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 1);
		countImg[2].GetComponent<CanvasGroup>().DOFade(1, 1);
		yield return new WaitForSeconds(1.0f);
		countImg[2].gameObject.SetActive(false);
		
		StartGame();
	}

	private void StartGame()
	{
		GameObject.Find("player").GetComponent<Runner>().canRun = true;
	}

}
