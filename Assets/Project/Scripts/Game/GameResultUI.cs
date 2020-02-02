

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultUI : MonoBehaviour {
    public GameObject lostNode;
    public GameObject winNode;
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if(GameResult.Result) {
            winNode.SetActive(true);
        } else {
            lostNode.SetActive(true);
        }
    }
    public void OnStartButton() {
        SceneManager.LoadScene("UI Tests");
    }
}