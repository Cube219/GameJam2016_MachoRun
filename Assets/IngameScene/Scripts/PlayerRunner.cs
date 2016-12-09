using UnityEngine;
using System.Collections;

public class PlayerRunner : Runner {

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	int tempD = 1;
	void FixedUpdate () {
		if(starting == true)
		runnerRun ();
		//Debug.Log (culSpeed);
		if (Input.GetKey (KeyCode.RightArrow))
			right = 1;
		if (Input.GetKey (KeyCode.LeftArrow))
			right = -1;
		if (jumping == true && Input.GetKeyDown (KeyCode.Space) && dJump == false) {
			runnerDoubleJump ();
			Debug.Log ("2단점프");

		}
		if (jumping == true && tempD != right && culSpeed < maxSpeed*1.7) {
			culSpeed += 2.0f;
			tempD = right;
		}
	}
	void OnCollisionStay2D(Collision2D coll)
	{
		jumping = false;
		dJump = false;
		if (coll.gameObject.tag == "wall" && Input.GetKey (KeyCode.Space)) {
			runnerJump ();
			Debug.Log ("1단점프");
		}
	}
}
