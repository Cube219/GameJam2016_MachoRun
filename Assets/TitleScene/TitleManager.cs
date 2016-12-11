using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class TitleManager : MonoBehaviour {
	public GameObject m;
	public GameObject mmRace;
	public GameObject toki;
	public GameObject space;

	bool intro = false;
	// Use this for initialization
	void Start () {
		startAnime ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space) && intro) {
			SceneManager.LoadScene ("LobbyScene");
		}
	}

	void startAnime()
	{
		StartCoroutine(Anime());
		//}
	}
	private IEnumerator Anime()
	{
		//m.transform.DO
		//m.GetComponent<Transform>().position.
		//m.transform.DOMove(new Vector3(2.84f,-1.5f,0f),0.5f,false);
		//yield return new WaitForSeconds(0.5f);
		m.transform.DOMove(new Vector3(2.2f,-0.73f,0f),0.5f,false);
		yield return new WaitForSeconds(0.5f);
		mmRace.transform.DORotate (new Vector3 (0f, 0f, 720f), 1f, RotateMode.LocalAxisAdd);
		mmRace.transform.DOMove (new Vector3 (0f, 0.3f, 0f), 1, false);
		yield return new WaitForSeconds(1f);
		toki.GetComponent<SpriteRenderer> ().DOFade (1f, 0.5f);

		space.SetActive (true);
		intro = true;
		yield return new WaitForSeconds(0.5f);
	}
}
