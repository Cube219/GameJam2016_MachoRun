using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EndZone : NetworkBehaviour {

	[ServerCallback]
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Player") {
			GameManager.m.Victory(coll.gameObject.GetComponent<Runner>());
		}
	}
}
