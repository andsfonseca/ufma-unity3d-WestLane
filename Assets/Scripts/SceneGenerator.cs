﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour {

    [Header("Phases")]
    public SceneObject[] phases;

    [Header("Initial Phase")]
    public SceneObject initialPhase;

    [Header("Phase Sequence")]
    public string phaseProgression;

    //Bloco que será instanciado
    private GameObject m_block;

    //Sequencia de Fases que serão instaciadas
    private List<SceneObject> m_PhaseProgressionList;

    //Iterator das Fases que serão instaciadas
    private int m_PhaseProgressionCurrent;

    //Fase Atual carregada
    private SceneObject m_currentPhase;

    //Proxima fase a ser carregada
    private SceneObject m_nextPhase;

    //Linha da Fase atual
    private int m_currentLine;

    //Booleana indicativa se as fases geradas serão randomicas
    private bool m_isRandom;

    ///<summary>
    ///Recupera a fase atual
    ///</summary>
    public SceneObject CurrentPhase {
        get {
            return m_currentPhase;
        }
    }

    void Start() {

        //Se a Fase inicial não foi carregada
        if (initialPhase == null) {
            Debug.Log("A fase inicial não foi cadastrada");
            gameObject.SetActive(false);
            return;
        }

        //Se não existir fases carregadas
        if (phases.Length == 0) {
            Debug.Log("Nenhuma Fase Cadastrada");
        }
    }

    /// <summary>
    /// Gera um cenário inicial com o tamanho delimitado pelas linhas
    /// </summary>
    /// <param name="lines">Numero de linhas a ser instaciado</param>
	public void StartGeneration(int lines) {

        //Caso tenha Escrito Random, as fases serão aleatórias
        if (phaseProgression == "random") {
            m_isRandom = true;
        }
        // Caso não elas serão instanciado a partir da lista de fases registradas
        else {
            m_PhaseProgressionList = new List<SceneObject>();
            foreach (string s in phaseProgression.Split(',')) {
                m_PhaseProgressionList.Add(phases[int.Parse(s)]);
            }

            m_PhaseProgressionCurrent = 0;

        }

        m_currentLine = 0;
        m_currentPhase = initialPhase;
        for (int i = 1; i > lines; i++) {
            GenerateLine();
        }
    }

    /// <summary>
    /// Gera uma linha do cenário
    /// </summary>
	private void GenerateLine() {

    }
    /// <summary>
    /// Avança de Cena e arruma o cenário para uma próxima Cena
    /// </summary>
    private void getNextScene() {
        m_currentPhase = m_nextPhase;
        m_nextPhase = getScene();
        m_currentLine = 0;
    }

    /// <summary>
    /// Recupera Uma SceneObject Setado pelo atributos públicos do SceneGenerator
    /// </summary>
    /// <returns>Retorna um SceneObject com uma Cena definida</returns>
    private SceneObject getScene() {
        SceneObject aux;

        //Se estiver o random como true, Pega uma fase aleatória
        if (m_isRandom) {
            aux = phases[AndsLib.Util.Random.Range(0, phases.Length)];
        }
        //Caso não
        else {
            //Se o current já estiver ultrapassado o limite, ele volta a 0
            if (m_PhaseProgressionCurrent > (m_PhaseProgressionList.Count - 1)) {
                m_PhaseProgressionCurrent = 0;
            }

            //Pega a Scene Setada pelo Current 
            aux = m_PhaseProgressionList[m_PhaseProgressionCurrent];
        }

        return aux;
    }


}
