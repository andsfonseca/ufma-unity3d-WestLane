using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinState : GameState {

	public GameWinState() : base(EGameState.GAMEWIN) {
	}

	public override void UpdateState() {

	}

	public override void OnStartGameState() {
		base.OnStartGameState();
		GameLogic.Instance.gameStateManager.GameWinState.SetActive(true);
	}

	public override void OnChangeGameState() {
		base.OnChangeGameState();
		GameLogic.Instance.gameStateManager.GameWinState.SetActive(false);
	}
}
