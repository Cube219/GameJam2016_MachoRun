using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Runner : MonoBehaviour {
	protected float maxSpeed = 10.0f;
	protected float culSpeed = 5.0f;
	protected float jumpHeight= 150.0f;
	protected float dJumpHeight= 200.0f;
	protected bool jumping = false;
	protected bool dJump = false;
	protected float accelSpeed= 0.05f;
	protected int upgrade = 0;
	protected int right = 1;

	public bool starting = false;

    void Start ()
	{
		
	}
		

	protected void runnerRun()
	{
		
		this.GetComponent<Transform>().Translate(new Vector2 (right, 0) * culSpeed * Time.deltaTime);
		if (culSpeed < maxSpeed) {
			culSpeed += accelSpeed;
		}

	}
	protected void endOfMap()
	{
		right = -1;
	}
	protected void runnerJump()
	{
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpHeight);
		jumping = true;
		culSpeed -= 1.0f;
	}
	protected void runnerDoubleJump()
	{
		dJump = true;
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * dJumpHeight);
	}

}
