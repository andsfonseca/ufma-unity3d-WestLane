using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState {

    protected bool m_isEnabled;
    protected EGameState m_gamestate;

    public EGameState name { get { return m_gamestate; } }

    public abstract void UpdateState();

    public virtual void OnStartGameState() {
        m_isEnabled = true;
    }
    public virtual void OnChangeGameState() {
        m_isEnabled = false;
    }

    public GameState(EGameState idGameState){
        m_gamestate = idGameState;
    }
}
