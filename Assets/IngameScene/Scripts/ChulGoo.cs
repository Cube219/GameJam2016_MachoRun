using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using DG.Tweening;

public class ChulGoo : NetworkBehaviour {

	private Rigidbody2D rigid;
	public bool isDown = false;

	private Vector2 initPos;

	// Use this for initialization
	void Start () {
		initPos = this.GetComponent<Transform>().position;

		rigid = this.GetComponent<Rigidbody2D>();
		rigid.isKinematic = true;

		StartCoroutine(ChulGoo_Down());
	}
	private IEnumerator ChulGoo_Down()
	{
		yield return new WaitForSeconds(4f);

		isDown = true;
		rigid.isKinematic = false;
		rigid.AddForce(Vector2.down * 600);
	}


	private IEnumerator Chulgoo_Up()
	{
		yield return new WaitForSeconds(2f);
		isDown = false;

		this.transform.DOMove(initPos, 5f);
		yield return new WaitForSeconds(5f);

		StartCoroutine(ChulGoo_Down());
	}

	[Server]
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "floor") {
			rigid.velocity = Vector2.zero;
			rigid.isKinematic = true;

			StartCoroutine(Chulgoo_Up());	
		}
	}
}
