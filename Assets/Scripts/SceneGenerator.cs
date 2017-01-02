using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour {

	[Header("Phases")]
	public SceneObject[] Phases;

	[Header("Initial Phase")]
	public SceneObject InitialPhase;

	[Header("LineBlock")]
	public GameObject LineBlocks;

	public string PhaseProgression;

	private List<SceneObject> m_PhaseProgressionList;
	private List<GameObject> m_LineBlocks;
	private SceneObject m_currentPhase;
	private int m_currentLine;
	private bool m_isRandom;

    ///<summary>
    ///Recupera a fase atual
    ///</summary>
    public SceneObject CurrentPhase{
		get{
			return m_currentPhase;
		}
	}

    ///<summary>
    ///Recupera a linha da fase atual
    ///</summary>
	public int CurrentLine{
		get{
			return m_currentLine;
		}
	}

    /// <summary>
    /// Gera um cenário inicial com o tamanho delimitado pelas linhas
    /// </summary>
    /// <param name="lines">Numero de linhas a ser instaciado</param>
	public void StartGeneration(int lines){
		
		if (PhaseProgression == "random") {
			m_isRandom = true;
		} 
		else {
			m_PhaseProgressionList = new List<SceneObject> ();
            foreach (string s in PhaseProgression.Split(',')) {
				m_PhaseProgressionList.Add (Phases [int.Parse (s)]);
			}
				
		}
		m_currentLine = 0;
		m_currentPhase = InitialPhase;

		for (int i = 1; i > lines; i++) {
			GenerateLine ();
		}
	}

    /// <summary>
    /// Gera uma linha do cenário
    /// </summary>
	public void GenerateLine(){
		//TODO: Definir NovaPosição
		Vector3 nextPosition = new Vector3(0,0,0);

		foreach (GameObject go in m_LineBlocks) {
			if (!go.activeSelf) {
				go.SetActive (true);

				//TODO: Ajustar a nova posição
				//TODO: Gerar Sprites
				return;
			}
		}

		GameObject lb = Instantiate (LineBlocks, nextPosition , Quaternion.identity);
		//TODO: Gerar Sprites
	}


}
