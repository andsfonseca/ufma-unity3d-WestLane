using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	private GameObject p1;
	private GameObject p2;
	//private Vector3 offset;
	private float x;
	private float y;
	// Use this for initialization
	void Start () {
		p1 = GameLogic.Instance.player1.gameObject;
		p2 = GameLogic.Instance.player2.gameObject;
		//offset = new Vector3 ((p1.transform.position.x - p2.transform.position.x) - this.transform.position.x, p1.transform.position.y - this.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		
		x = 0;
		y = Mathf.Max(p1.transform.position.y , p2.transform.position.y) ;
		Debug.Log ("x = "+x+" - y = "+y);
		this.transform.position = new Vector3(x, y);
	}
}
