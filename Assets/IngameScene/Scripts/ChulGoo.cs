using UnityEngine;
using System.Collections;

public class ChulGoo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag ("runner").transform.position.x >= this.transform.position.x-7) {
			this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "floor") {
			this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up*350);
			//Destroy (this);
		}
	}
}
