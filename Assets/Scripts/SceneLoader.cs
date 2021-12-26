using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene() {
        SceneManager.LoadScene(1);
    }
    public void LoadLoseScene() {
        SceneManager.LoadScene(2);
    }
    public void LoadWinScene() {
        SceneManager.LoadScene(3);
    }
    
}
