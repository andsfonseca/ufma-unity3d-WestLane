using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStateManager : MonoBehaviour{

    private GameState m_currentGameState;
    private GameState m_lastGameState;

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
    }


	/* Menu */
    public void BtnPlay() {
        SwitchGameState(new GameplayState());
    }

	/* Gameplay */
	public void BtnEnd(){
		SwitchGameState(new GameWinState ());
	}

	/*GameWin*/
	//Restart Play (Retry)
	public void BtnMenu(){
		SwitchGameState(new MenuGameState());
	}


}

