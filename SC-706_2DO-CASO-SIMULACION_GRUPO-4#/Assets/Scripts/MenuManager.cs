using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string gameScene;

    public void StartGame()
    {
        if (Time.timeScale <= 0.0F)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(gameScene);
    }
}
