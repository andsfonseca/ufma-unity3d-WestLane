﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class SceneObject : MonoBehaviour {

    [Header("Phase Mold")]
    public string[] lanes;

    [Header("Sprites")]
    public Sprite[] sprites;

    private int[][] m_matriz = null;

    [HideInInspector]
    [SerializeField]
    public List<GameObject> m_customizedElements;
    [HideInInspector]
    [SerializeField]
    public List<int> m_customizedElementsComplement;
    //public string[] osbstaculo;
    //public char especial;
    //public string[] hole;



    public void generateMatriz() {
        //contador para saber o quanto ja percorri
        int count = 0;
        //matriz com o numero de linhas (altura)
        m_matriz = new int[lanes.Length][];

        //for para pegar cada string q eu coloquei
        foreach (string s in lanes) {
            // contador para saber o numero de colunas percorridas
            int conta = 0;
            //pega a string e separa os elementos colocando-os em outro vetor (aux)
            string[] aux = s.Split(',');
            // cria as colunas da matriz
            m_matriz[count] = new int[aux.Length];
            //pega os elementos de aux
            foreach (string st in aux) {
                //coloca as strings, tranformadas em inteiros, para a matriz
                m_matriz[count][conta] = Int32.Parse(st);
                conta++;
            }
            count++;
        }
    }

    public Sprite getSprite(int line, int column) {

        if (m_matriz == null) generateMatriz();
        if (line < m_matriz.Length) {
            if (column < m_matriz[line].Length) {
                int number = m_matriz[line][column];
                if (number < sprites.Length) {
                    return sprites[number];
                }
            }
        }
        return null;
    }

    public GameObject getGameobject(int line, int column) {
        if (m_matriz == null) generateMatriz();
        if (line < m_matriz.Length) {
            if (column < m_matriz[line].Length) {
                int number = m_matriz[line][column];
                if (number < sprites.Length) {
                    if (m_customizedElementsComplement.Contains(number)) {
                        return m_customizedElements[m_customizedElementsComplement.IndexOf(number)];
                    }
                }
            }
        }
        return null;
    }

    public bool OnPlayerTryTouch(int line, int column) {

        if (m_matriz == null) generateMatriz();
        //testa os espaços na matriz de acordo com os numeros que representam esses espaços 
		switch (m_matriz [line] [column]) {
			case 6:
				return false;
					
			case 8:
				return false;
		}
		return true;
    }


	public void doAction(int line, int column){
		float time;

		switch (m_matriz [line] [column]) {
			
			//speed
			case 12:
				StartCoroutine (SpeedUp ());
				//return true;
				break;

			//slow
			case 15:
				StartCoroutine (Slow ());
				break;

			case 18:
				StartCoroutine (Freeze ());
				break;
		}
	}

	IEnumerator SpeedUp(){
		if (Input.GetKeyDown("v")){
			//Acelera
			GameLogic.Instance.player1.setSpeed(0.04f);
			yield return new WaitForSeconds(3);
			//Desacelera
			GameLogic.Instance.player1.setSpeed(0.02f);
		}

		if (Input.GetKeyDown ("m")) {
			//Acelera
			GameLogic.Instance.player2.setSpeed(0.04f);
			yield return new WaitForSeconds(3);
			//Desacelera
			GameLogic.Instance.player2.setSpeed(0.02f);
		}
	}


	IEnumerator Slow(){
		if (Input.GetKeyDown("v")){
			//Acelera
			GameLogic.Instance.player2.setSpeed(0.01f);
			yield return new WaitForSeconds(3);
			//Desacelera
			GameLogic.Instance.player2.setSpeed(0.02f);
		}

		if (Input.GetKeyDown ("m")) {
			//Acelera
			GameLogic.Instance.player1.setSpeed(0.01f);
			yield return new WaitForSeconds(3);
			//Desacelera
			GameLogic.Instance.player1.setSpeed(0.02f);
		}
	}


	IEnumerator Freeze(){
		if (Input.GetKeyDown("v")){
			//Acelera
			GameLogic.Instance.player2.setSpeed(0.00f);
			yield return new WaitForSeconds(2);
			//Desacelera
			GameLogic.Instance.player2.setSpeed(0.02f);
		}

		if (Input.GetKeyDown ("m")) {
			//Acelera
			GameLogic.Instance.player1.setSpeed(0.00f);
			yield return new WaitForSeconds(2);
			//Desacelera
			GameLogic.Instance.player1.setSpeed(0.02f);
		}
	}
}

[CustomEditor(typeof(SceneObject))]
public class SceneObjectEditor : Editor
{
    bool showAllGameObjects = false;
    bool showCreateGameObject = false;

    int tile;
    GameObject go;
    public override void OnInspectorGUI()
    {
        SceneObject so = (SceneObject)target;

        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Tiles With Game Objects", EditorStyles.boldLabel);
        showAllGameObjects = EditorGUILayout.Foldout(showAllGameObjects, "Show All");
        if (showAllGameObjects)
        {
            for (int i = 0; i < so.m_customizedElements.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Tile: " + so.m_customizedElementsComplement[i]);
                so.m_customizedElements[i] = EditorGUILayout.ObjectField(so.m_customizedElements[i], typeof(GameObject), true) as GameObject;
                if (GUILayout.Button("Remove")) {
                    so.m_customizedElements.RemoveAt(i);
                    so.m_customizedElementsComplement.RemoveAt(i);
                    Debug.Log("GameObject removido do Tile");
                    Repaint();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        showCreateGameObject = EditorGUILayout.Foldout(showCreateGameObject, "Create New");
        if (showCreateGameObject)
        {
                
            tile = EditorGUILayout.IntField("Tile Number", tile);
            go = EditorGUILayout.ObjectField("GameObject",go, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("Add GameObject")) {
                if (tile < so.sprites.Length)
                {
                    so.m_customizedElements.Add(go);
                    so.m_customizedElementsComplement.Add(tile);
                    Debug.Log("GameObject associado ao Tile");
                    //Repaint();
                }
                else {
                    Debug.Log("Tile Inexistente");
                }
            }
        }
    }

}
