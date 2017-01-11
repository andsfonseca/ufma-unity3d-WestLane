using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour{
    public Text TextDistance;//campo de texto a ser exibido
    public double distanceCalc;//armazena distância atual

    private GameState m_currentGameState;
    private GameState m_lastGameState;

    void Start() {
        //Dist = GetComponent<Text>();
        //TextDistance = GetComponent<Text>();
        UpdateDistance();
    }

    public EGameState current {
        get {
            return m_currentGameState.name;
        }
    }

    public EGameState last {
        get {
            return m_lastGameState.name;
        }
    }

    public void SwitchGameState(GameState gs) {
        if (m_currentGameState != null) m_lastGameState = m_currentGameState;
        if (m_currentGameState != null) m_currentGameState.OnChangeGameState();
        m_currentGameState = gs;
        m_currentGameState.OnStartGameState();
    }

    void Update() {
        if (m_currentGameState != null) m_currentGameState.UpdateState();
        UpdateDistance();
    }


    public void BtnPlay() {
        SwitchGameState(new GameplayState());
    }

    [Header("Canvas")]
    public GameObject MenuState;
    public GameObject GameplayState;
    public GameObject GameWinState;

    [Header("Distance multiplier")]
    public double distanceMultiplier;

    void UpdateDistance() {
        //trocar pelo método que pega do player
        if (GameLogic.Instance.gameStateManager.current == EGameState.PLAYING) {
            if ((Camera.main.gameObject.transform.position.y) > 0) {
                distanceCalc = (float.Parse(Camera.main.gameObject.transform.position.y.ToString()) * distanceMultiplier);
            }
            else {
                distanceCalc = 0;
            }
            Debug.Log(distanceCalc);
            this.TextDistance.enabled = true;
            this.TextDistance.text = ((int) distanceCalc).ToString() + " m";

        }
        else {
            this.TextDistance.enabled = false;
        }
    }
}






   