using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Gashi : NetworkBehaviour {

	public BoxCollider2D approachCollider;
	public GameObject needle;

	[SyncVar]
	private bool under = true;

	
	// Update is called once per frame
	[ServerCallback]
	void Update () {
		if(isServer == true && under == true) {
			foreach(Runner r in GameManager.runners) {
				if(approachCollider.IsTouching(r.GetComponent<Collider2D>())) {
					Vector3 tr = new Vector3(needle.transform.position.x, needle.transform.position.y + 0.8f, 0.0f);
					needle.transform.position = tr;
					under = false;
					RpcShowGashi();
				}
			}
		}
	}

	[ClientRpc]
	private void RpcShowGashi()
	{
		if(!isServer) {
			Vector3 tr = new Vector3(needle.transform.position.x, needle.transform.position.y + 0.8f, 0.0f);
			needle.transform.position = tr;
			under = false;
		}
	}
}
