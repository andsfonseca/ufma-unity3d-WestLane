﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private float speed;
	private GameObject player;
	//private int v1;
	private float tamanhoDoTile;
	private float offset;
	private int tileAtual;
	private int tileMax;
	private int tileMin;
	private string nome;
	private float[] tilePositions;

	// Use this for initialization
	void Start () {
		
		player = this.gameObject;//this.transform.parent.gameObject;
		tamanhoDoTile = 200f;
		offset = 0f;
		nome = this.name;
		tileMax = 3;
		tileMin = 0;
		if ((string.Compare (name, "Player1")) == 0) {
			tilePositions = GameLogic.Instance.sceneGenerator.getLanePositions ();
			tileAtual = 2;
		} else {
			tilePositions = GameLogic.Instance.sceneGenerator.getReverseLanePositions ();
			tileAtual = 1;
		}
		Debug.Log ("tile max do " + nome + " = " + tileMax);
		speed = 0.02f;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameLogic.Instance.gameStateManager.current == EGameState.PLAYING) {
            player.transform.Translate(new Vector3(0, this.speed));
            Mov(offset, tamanhoDoTile, nome, tileAtual, tileMax, tileMin);
        }
	}

	private void Mov(float offset, float tile, string name, int tA, int tMax, int tMin){
		float movX;
		//Debug.Log ("tile atual do " + name + " = " + this.tileAtual);
		//float m = (float)(0.01*(offset+tile));
		//v1 = (int)Mathf.Ceil(m);
		if ((string.Compare (name, "Player1"))==0) {
			if (Input.GetKeyDown (KeyCode.A)) {
				if (tA > tMin) {
					//player.transform.Translate (new Vector3 (-m, 0));
					this.tileAtual = this.tileAtual - 1;
				}
			} else if (Input.GetKeyDown (KeyCode.D)) {
				if (tA < tMax) {
					//player.transform.Translate (new Vector3 (m, 0));
					this.tileAtual = this.tileAtual + 1;
				}
			}
		}else if ((string.Compare (name, "Player2"))==0) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				if (tA > tMin) {
					//player.transform.Translate (new Vector3 (-m, 0));
					this.tileAtual = this.tileAtual - 1;
				}
			} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				if (tA < tMax) {
					//player.transform.Translate (new Vector3 (m, 0));
					this.tileAtual = this.tileAtual + 1;
				}
			}
		}
		movX = tilePositions [tileAtual];
		player.transform.position = new Vector3 (movX, this.transform.position.y);
	}
	public bool PodeMov(int tileAtual, int tileEsq, int tileDir, int tileFrent, KeyCode tecla){
		if(tecla==KeyCode.A||tecla==KeyCode.LeftArrow){
			if(tileEsq != 0){
				return true;
			}
		}else if(tecla==KeyCode.D||tecla==KeyCode.RightArrow){
			if(tileDir != 0){
				return true;
			}
		}else{
			if(tileFrent!=0){
				return true;
			}
		}	
		return false;
	}

	public float getSpeed(){
		return this.speed;
	}
		
	public void setSpeed(float speed){
		this.speed = speed;
	}
}
