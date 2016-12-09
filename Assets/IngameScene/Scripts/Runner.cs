using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Runner : MonoBehaviour {
	protected float maxSpeed = 10.0f;
	protected float culSpeed = 5.0f;
	protected float jumpHeight= 250.0f;
	protected float dJumpHeight= 500.0f;
	protected bool jumping = false;
	protected bool dJump = false;
	protected float accelSpeed= 0.05f;
	protected int upgrade = 0;
	protected int right = 1;

	public bool starting = false;


    void Start ()
	{
		
	}
		
	void Update()
	{
		Debug.Log (this.GetComponent<Rigidbody2D> ().velocity);
		//Debug.Log("" + culSpeed);
	}

	protected void runnerRun()
	{
		
		this.GetComponent<Transform>().Translate(new Vector2 (right, 0) * culSpeed * Time.deltaTime);
		if (culSpeed < maxSpeed) {
			culSpeed += accelSpeed;
		} else
			culSpeed -= accelSpeed*1.5f;

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
		//float vx = this.GetComponent<Rigidbody2D>().velocity.x;
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 0));
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * dJumpHeight);
	}

}
