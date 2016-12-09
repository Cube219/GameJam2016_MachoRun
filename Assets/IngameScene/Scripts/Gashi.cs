using UnityEngine;
using System.Collections;

public class Gashi : MonoBehaviour {
	bool under = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindWithTag("runner").transform.position.x >= this.transform.position.x && under == true)
		{
			Vector3 tr = new Vector3 (this.transform.position.x, this.transform.position.y + 0.5f, 0.0f);
			this.transform.position = tr;
			under = false;
		}
	
	}
}
