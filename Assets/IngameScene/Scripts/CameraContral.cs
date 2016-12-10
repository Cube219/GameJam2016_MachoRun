using UnityEngine;
using System.Collections;

public class CameraContral : MonoBehaviour {
	public GameObject player;
	/*
	// Use this for initialization
	void Start()
	{
		foreach(Runner r in GameManager.runners) {
			if(r.isLocalPlayer) {
				player = r.gameObject;
				break;
			}
		}
		if(player != null) {
			Transform t = player.transform;
			Vector3 playerPos = new Vector3(t.position.x, 3.2f, -10);
			this.GetComponent<Transform>().position = playerPos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player == null) {
			foreach(Runner r in GameManager.runners) {
				if(r.isLocalPlayer) {
					player = r.gameObject;
					break;
				}
			}
		}

		if(player != null) {
			Transform t = player.transform;
			Vector3 playerPos = new Vector3(t.position.x, this.transform.position.y, -10);
			this.GetComponent<Transform>().position = playerPos;
		}

	}*/
}
