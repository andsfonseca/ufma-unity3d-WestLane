using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneObject : MonoBehaviour {

	[Header("Phase Mold")]
	public string[] lanes;

    [Header("Sprites")]
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

    public Sprite getSprite(int line, int column) {

        if (m_matriz == null) generateMatriz();
        if (line < m_matriz.Length) {
            if (column < m_matriz[line].Length){
                int number = m_matriz[line][column];
                if (number < sprites.Length)
                {
                    return sprites[number];
                }
            }
        }
        return null;
	}
		
	public bool OnPlayerTryTouch(int line,  int column){

        if (m_matriz == null) generateMatriz();
        //testa os espaços na matriz de acordo com os numeros que representam esses espaços 
        if (m_matriz [line] [column] == 1 || m_matriz [line] [column] == 2) {
			//retorna false indicando que esses numeros(espaços) não posso acessar-los
			return false;
		}
		else{
			return true;
		}
	}


	/*ublic bool doActiion(int line, int column){
		
	}*/

}