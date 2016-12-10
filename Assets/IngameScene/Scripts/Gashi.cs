using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Gashi : NetworkBehaviour {

	public BoxCollider2D approachCollider;
//	public BoxCollider2D killCollider;

	[SyncVar]
	private bool under = true;

	
	// Update is called once per frame
	[Server]
	void Update () {

		if(under == true) {
			foreach(Runner r in GameManager.runners) {
				if(approachCollider.IsTouching(r.GetComponent<Collider2D>())) {
					Vector3 tr = new Vector3(this.transform.position.x, this.transform.position.y + 0.8f, 0.0f);
					this.transform.position = tr;
					under = false;
				}
			}
		}
	}
}
