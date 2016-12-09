using UnityEngine;
using System.Collections;

public class CameraContral : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = new Vector3 (GameObject.Find ("player").transform.position.x, this.transform.position.y,-10);
		this.GetComponent<Transform> ().position = playerPos;

	}
}
