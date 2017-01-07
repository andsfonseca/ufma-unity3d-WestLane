using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneObject : MonoBehaviour {
	
	[Header("Phase Mold")]
	public string[] lanes;
	public Sprite[] sprites;
	private int[][] m_matriz = null;
	//public string[] osbstaculo;
	//public char especial;
	//public string[] hole;

	public void generateMatriz(){
		//contador para saber o quanto ja percorri
		int count = 0;
		//matriz com o numero de linhas (altura)
		m_matriz = new int[lanes.Length][];

		//for para pegar cada string q eu coloquei
		foreach (string s in lanes) {
			// contador para saber o numero de colunas percorridas
			int conta = 0;
			//pega a string e separa os elementos colocando-os em outro vetor (aux)
			string[] aux = s.Split (',');
			// cria as colunas da matriz
			m_matriz [count] = new int[aux.Length];
			//pega os elementos de aux
			foreach (string st in aux) {
				//coloca as strings, tranformadas em inteiros, para a matriz
				m_matriz [count] [conta] = Int32.Parse (st);
				conta++;
			}
			count++;
		}
	}

	public Sprite getSprite(int line,  int column){
		//Sprite sprite;
	
		switch (m_matriz [line] [column]) {
		case 1:
			return sprites [0];

		case 2:
			return sprites [1];

		case 3:
			return sprites [2];

		case 4:
			return sprites [3];

		case 5:
			return sprites [4];

		default:
			return null;
		}

	}

	/*m_parent.OnPlayerTryTouch(int line, int column){
		
	}*/


	/*public bool doAction(int line, int column){
		switch (m_matriz [line] [column]) {
			case 1:
				//realiza a ação de acordo com o numero da matriz e retorna true se conseguiu realiza-la 
				return "action1";
				break;

			case 2:
				return sprites [1];
				break;

			case 3:
				return sprites [2];
				break;

			case 4:
				return sprites [3];
				break;

			case 5:
				return sprites [4];
				break;
		}
	}*/
		


	public bool OnPlayerTryTouch(int line,  int column){
		
		//testa os espaços na matriz de acordo com os numeros que representam esses espaços 
		if (m_matriz [line] [column] == 1 || m_matriz [line] [column] == 2) {
			//retorna false indicando que esses numeros(espaços) não posso acessar-los
			return false;
		}
		else{
			return true;
		}
	}
}