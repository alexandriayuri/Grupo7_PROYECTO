using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSwitcher : MonoBehaviour
{
    public int sceneBuildIndex;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
