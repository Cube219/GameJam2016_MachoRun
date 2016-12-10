using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Runner : NetworkBehaviour {

	public TextMesh nameMesh;
	public GameObject macho;

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
	public bool canRun = false;
	[SyncVar]
	public bool isBumped = false;
	[SyncVar]
	public bool banana = false;
	[SyncVar]
	public bool slow = false;
	[SyncVar]
	public bool fast = false;
	[SyncVar]
	public int right = 1;

	public BoxCollider2D approachCollider;

	private float preX = 0;
	private int currentFloor = 1;

	void Awake()
	{
		GameManager.runners.Add(this);
	}

	void OnDestroy()
	{
		GameManager.runners.Remove(this);
	}

	void Start ()
	{
		nameMesh.text = playerName;
		nameMesh.color = playerColor;
	}

	private void SetCamera()
	{
		if(isLocalPlayer) {
			Transform t = this.transform;

			currentFloor = (int)((t.position.y - 0.9f) / 7.2f) + 1;

			Vector3 playerPos;
			if((t.position.y - 0.9f) / 7.2f - (float)(currentFloor - 1) >= 0.4f)
				playerPos = new Vector3(t.position.x, t.position.y, -10);
			else
				playerPos = new Vector3(t.position.x, 3.2f + (currentFloor-1)*7.2f, -10);

			Vector2 cameraPos = Camera.main.GetComponent<Transform>().position;

			Camera.main.GetComponent<Transform>().position = new Vector3(playerPos.x, cameraPos.y, -10);
			Camera.main.GetComponent<Transform>().DOMoveY(playerPos.y, 0.5f);
		}
	}

	[ClientCallback]
	void Update()
	{
		SetCamera();
	}

	[SyncVar]
	float timer = 0f;

	[ClientCallback]
	void FixedUpdate()
	{
		if(isLocalPlayer) {
			if(fast == true) {
				currentSpeed += 10f;
				fast = false;
			}
			if(Input.GetKey(KeyCode.LeftArrow)) {
				right = -1;
			}
			if(Input.GetKey(KeyCode.RightArrow)) {
				right = 1;
			}
			if(canRun == true && isBumped == false) {
				runnerRun();

				if(Input.GetKeyDown(KeyCode.Space)) {
					if(jumping == false)
						runnerJump();
					else if(jumping == true && dJump == false)
						runnerDoubleJump();
				}
				/*
				if(Input.GetKeyDown(KeyCode.LeftArrow) && jumping == true && currentSpeed < maxSpeed * 1.5f) {
					//this.transform.FindChild("GameObject").transform.localScale = new Vector2 (-2, 2);
					//right = -1;
					currentSpeed += 2f;
				}
				if(Input.GetKeyDown(KeyCode.RightArrow) && jumping == true && currentSpeed < maxSpeed * 1.5f) {
					//this.transform.FindChild("GameObject").transform.localScale = new Vector2 (2, 2);
					//right = 1;
					currentSpeed += 2f;
				}*/
			}
			if(slow == true) {
				Debug.Log("됩니다");
				timer += Time.deltaTime;
				if(timer <= 3f) {
					maxSpeed = 2f;
					Debug.Log("타이머도");
				} else
					slow = false;
			}
			if(approachCollider.IsTouching(this.GetComponent<Collider2D>())) {
				slow = true;
			}
		}

		if(!isBumped) {
			if(preX - transform.position.x < -0.05f) {
				macho.transform.localScale = new Vector3(1.5f, 1.5f, 1);
			} else if(preX - transform.position.x > 0.05f) {
				macho.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
			}
		}

		preX = transform.position.x;
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
		else if (coll.gameObject.tag == "fast") {
			fast = true;
			Destroy (coll.gameObject);
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
		if(isBumped == false)
			StartCoroutine(Bumbed_c());
	}
	private IEnumerator Bumbed_c()
	{
		isBumped = true;
		this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.GetComponent<Rigidbody2D>().AddForce(new Vector2(right*-230f, 230f));
		yield return new WaitForSeconds(1.1f);
		isBumped = false;

	}

}
