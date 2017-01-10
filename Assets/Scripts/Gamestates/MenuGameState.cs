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
        GameLogic.Instance.MenuState.SetActive(true);
    }

    public override void OnChangeGameState() {
        base.OnChangeGameState();
        GameLogic.Instance.MenuState.SetActive(false);
    }

}
