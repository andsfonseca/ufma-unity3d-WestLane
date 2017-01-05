using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour {
	private GameObject player;
	private int v1;
	private int tamanhoDoTile;
	private int offset;
	private int tileAtual;
	private int tileMax;
	private string nome;
	// Use this for initialization
	void Start () {
		player = this.gameObject;//this.transform.parent.gameObject;
		tamanhoDoTile = 200;
		offset = 0;
		nome = this.name;
		tileMax = 2;
		if ((string.Compare (name, "p2")) == 0) {
			tileAtual = 0;
		} else {
			tileAtual = 2;
		}
		Debug.Log ("tile max do " + nome + " = " + tileMax);
	}
	
	// Update is called once per frame
	void Update () {
		Mov (offset, tamanhoDoTile, nome, tileAtual, tileMax);
	}

	void Mov(int offset, int tile, string name, int tA, int tM){
		Debug.Log ("tile atual do " + name + " = " + this.tileAtual);
		float m = (float)(0.01*(offset+tile));
		v1 = (int)Mathf.Ceil(m);
		if ((string.Compare (name, "p1"))==0) {
			if (Input.GetKeyDown (KeyCode.A)) {
				if (tA > 0) {
					player.transform.Translate (new Vector3 (-v1, 0));
					this.tileAtual = this.tileAtual - 1;
				}
			} else if (Input.GetKeyDown (KeyCode.D)) {
				if (tA < tM) {
					player.transform.Translate (new Vector3 (v1, 0));
					this.tileAtual = this.tileAtual + 1;
				}
			}
		}else if ((string.Compare (name, "p2"))==0) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				if (tA > 0) {
					player.transform.Translate (new Vector3 (-v1, 0));
					this.tileAtual = this.tileAtual - 1;
				}
			} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				if (tA < tM) {
					player.transform.Translate (new Vector3 (v1, 0));
					this.tileAtual = this.tileAtual + 1;
				}
			}
		}
	}
		
}
