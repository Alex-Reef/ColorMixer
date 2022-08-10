using UnityEngine;
using System;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour {
    [SerializeField] public Button MixButton, RestartBtn, ContinueBtn, NextLevelBtn, NewGameBtn;

    private void Start(){
        MixButton.onClick.AddListener(MixButtonOnClick);
        RestartBtn.onClick.AddListener(RestartLevel);
        ContinueBtn.onClick.AddListener(ContinueLevel);
        NextLevelBtn.onClick.AddListener(NextLevel);
        NewGameBtn.onClick.AddListener(NewGame);
    } 

    private void MixButtonOnClick(){
        LevelsManager.Instance.LevelPassed();
    }

    private void RestartLevel(){
        LevelsManager.Instance.RestartLevel();
    }

    private void ContinueLevel(){
        LevelsManager.Instance.ContinueLevel();
    } 

    private void NextLevel(){
        LevelsManager.Instance.NextLevel();
    }

    private void NewGame(){
        LevelsManager.Instance.NewGame();
    }
}