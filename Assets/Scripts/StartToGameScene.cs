using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToGameScene : MonoBehaviour
{
    public void ChangeSceneToGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeSceneToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
