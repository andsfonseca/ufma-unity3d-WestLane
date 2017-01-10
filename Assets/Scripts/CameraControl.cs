using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	private GameObject p1;
	private GameObject p2;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		p1 = GameLogic.Instance.player1.gameObject;
		p2 = GameLogic.Instance.player2.gameObject;
		offset = new Vector3 ((p1.transform.position.x - p2.transform.position.x) - this.transform.position.x, p1.transform.position.y - this.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3((p1.transform.position.x - p2.transform.position.x)+offset.x, (p1.transform.position.y - p2.transform.position.y)+offset.y);
	}
}
