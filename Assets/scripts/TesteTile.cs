using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteTile : MonoBehaviour {
	private int indice;
	public float positionY;
	// Use this for initialization
	void Start () {
		string name = this.tag;

		if ((string.Compare (name, "tile0")) == 0) {
			this.indice = 0;
		}else if ((string.Compare (name, "tile1")) == 0) {
			this.indice = 1;
		}else if ((string.Compare (name, "tile2")) == 0) {
			this.indice = 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
		MoveTile ();
	}
	/*public void teste(){
		Debug.Log ("clicado");
	}*/

	private void MoveTile(){
		this.gameObject.transform.Translate (new Vector3(0,-0.02f));
		positionY = this.gameObject.transform.position.y;
		if (positionY < -5f) {
			this.gameObject.transform.Translate (new Vector3 (0, 8f));
		}
	}




	public int GetIndice(){
		return this.indice;
	}
	public void SetIndice(int n){
		this.indice = n;
	}

}
