using UnityEngine;
using System.Collections;

public class CameraContral : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		Transform t = GameObject.Find("player").transform;
		Vector3 playerPos = new Vector3(t.position.x, 3, -10);
		this.GetComponent<Transform>().position = playerPos;
	}
	
	// Update is called once per frame
	void Update () {
		Transform t = GameObject.Find("player").transform;
		Vector3 playerPos = new Vector3 (t.position.x, this.transform.position.y,-10);
		this.GetComponent<Transform> ().position = playerPos;

	}
}
