using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else
        {
            Debug.Log("Coming Soon...");
        }
    }

    public void PreviousScene()
    {
        if (SceneManager.sceneCountInBuildSettings < SceneManager.GetActiveScene().buildIndex - 1)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
