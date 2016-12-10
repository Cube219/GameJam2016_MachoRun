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
	protected float jumpHeight= 400f;
	[SyncVar]
	protected float dJumpHeight= 400f;
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
	[SyncVar]
	public bool banana = false;
	[SyncVar]
	public bool slow = false;
	[SyncVar]
	public bool fast = false;


	void Awake()
	{
		GameManager.runners.Add(this);
	}

	void Start ()
	{
		nameMesh.text = playerName;
		nameMesh.color = playerColor;
	}

	[ClientCallback]
	void Update()
	{
	}

	[SyncVar]
	float timer = 0f;

	[ClientCallback]
	void FixedUpdate()
	{
		if (Input.GetKey (KeyCode.LeftArrow)) {
			right = 1;
			this.transform.localScale = new Vector2 (-2, 2);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			right = 1;
			this.transform.localScale = new Vector2 (2, 2);
		}
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
		if (slow == true) {
			timer += Time.deltaTime;
			if (timer <= 3f)
				maxSpeed = 2f;
			else
				slow = false;
		}
	}

	[ClientCallback]
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "floor") {
			// 점프들 초기화
			jumping = false;
			dJump = false;
		} else if (coll.gameObject.tag == "obstacle") {
			runnderBumped ();
		} else if (coll.gameObject.tag == "banana") {
			banana = true;
			Destroy (coll.gameObject);
		}
		else if (coll.gameObject.tag == "slow") {
			slow = true;
			timer = 0f;
		}
	}

	public void Init()
	{
	}

	protected void runnerRun()
	{
		
		this.transform.Translate(new Vector2 (right, 0) * currentSpeed * Time.deltaTime);
		if (banana == true) {
			if (currentSpeed > 0)
				currentSpeed -= accelSpeed * 1.2f;
			else
				banana = false;
		}
		else {
			if (currentSpeed < maxSpeed) {
				currentSpeed += accelSpeed;
			} else
				currentSpeed -= accelSpeed * 1.5f;
		}
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
		this.GetComponent<Rigidbody2D>().AddForce(new Vector2(right*-230f, 230f));
		yield return new WaitForSeconds(1.1f);
		isBumped = false;

	}

}
