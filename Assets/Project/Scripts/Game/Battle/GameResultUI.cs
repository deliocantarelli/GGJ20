

using GGJ20.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameResultUI : MonoBehaviour {

    [Inject]
    private GameStateController controller;
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
    }
    public void OnStartButton() {

        controller.StartRun();
        controller.GoToFloorScene();
    }
}