using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameState : GameState {

    public MenuGameState() : base(EGameState.MENU) {
    }

    public override void UpdateState() {
       
    }

    public override void OnStartGameState() {
        base.OnStartGameState();
        GameLogic.Instance.gameStateManager.MenuState.SetActive(true);
    }

    public override void OnChangeGameState() {
        base.OnChangeGameState();
        GameLogic.Instance.gameStateManager.MenuState.SetActive(false);
    }

}
