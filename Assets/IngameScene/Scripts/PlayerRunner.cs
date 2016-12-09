using UnityEngine;
using System.Collections;

public class PlayerRunner : Runner {

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(starting == true)
		runnerRun ();
		//Debug.Log (culSpeed);

		if (jumping == true && Input.GetKeyDown (KeyCode.Space) && dJump == false) {
			runnerDoubleJump ();
			Debug.Log ("2단점프");
		}
	}
	void OnCollisionStay2D(Collision2D coll)
	{
		jumping = false;
		dJump = false;
		if (coll.gameObject.tag == "wall" && Input.GetKey (KeyCode.Space)) {
			runnerJump ();
			Debug.Log ("1단점프  "+jumping+dJump);
		}
	}
}
