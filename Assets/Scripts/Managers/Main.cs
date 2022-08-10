using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    public static Main Instance;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        PedManager.Instance.InitPeds();
        LevelsManager.Instance.StartLevel();
    }
}