using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : GameState {

    public GameplayState() : base(EGameState.PLAYING) {
    }

    public override void UpdateState() {

    }

    public override void OnStartGameState() {
        base.OnStartGameState();
        GameLogic.Instance.gameStateManager.GameplayState.SetActive(true);
    }

    public override void OnChangeGameState() {
        base.OnChangeGameState();
		GameLogic.Instance.gameStateManager.GameplayState.SetActive(false);
    }

}
