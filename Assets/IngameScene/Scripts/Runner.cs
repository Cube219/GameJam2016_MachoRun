using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class Runner : NetworkBehaviour {

	public TextMesh nameMesh;

	[SyncVar]
	public string playerName;
	[SyncVar]
	public Color playerColor;

	[SyncVar]
	protected float maxSpeed = 4.0f;
	[SyncVar]
	protected float currentSpeed = 3.0f;
	[SyncVar]
	protected float jumpHeight= 350f;
	[SyncVar]
	protected float dJumpHeight= 350f;
	[SyncVar]
	protected bool jumping = false;
	[SyncVar]
	protected bool dJump = false;
	[SyncVar]
	protected float accelSpeed= 0.05f;
	[SyncVar]
	protected int upgrade = 0;
	[SyncVar]
	protected int right = 1;

	[SyncVar]
	public bool canRun = false;
	[SyncVar]
	public bool isBumped = false;


	void Awake ()
	{
		GameManager.runners.Add(this);
	}

	void Start()
	{
		nameMesh.text = playerName;
		nameMesh.color = playerColor;
	}

	void OnDestroy()
	{
		GameManager.runners.Remove(this);
	}
		
	[ClientCallback]
	void FixedUpdate()
	{
		if(!isLocalPlayer)
			return;

		if(canRun == true && isBumped == false) {
			runnerRun();

			if(Input.GetKeyDown(KeyCode.Space)) {
				if(jumping == false)
					runnerJump();
				else if(jumping == true && dJump == false)
					runnerDoubleJump();
			}
			
			if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				right = -1;
			}
			if(Input.GetKeyDown(KeyCode.RightArrow)) {
				right = 1;
			}
		}
	}
	[ClientCallback]
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "floor") {
			// 점프들 초기화
			jumping = false;
			dJump = false;
		} else if(coll.gameObject.tag == "obstacle") {
			runnderBumped();
		}
	}

	public void Init()
	{
	}

	protected void runnerRun()
	{
		
		this.transform.Translate(new Vector2 (right, 0) * currentSpeed * Time.deltaTime);

		if (currentSpeed < maxSpeed) {
			currentSpeed += accelSpeed;
		} else
			currentSpeed -= accelSpeed*1.5f;

	}

	protected void runnerJump()
	{
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpHeight);
		jumping = true;
		currentSpeed -= 1.0f;
	}

	protected void runnerDoubleJump()
	{
		dJump = true;
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 0));
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * dJumpHeight);
	}

	protected void runnderBumped()
	{
		StartCoroutine(Bumbed_c());
	}
	private IEnumerator Bumbed_c()
	{
		isBumped = true;
		this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400f, 400f));
		yield return new WaitForSeconds(1.5f);
		isBumped = false;

	}

}
