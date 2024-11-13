using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    private const string MainSceneName = "Main";
    public void LoadMainScene()
    {
        SceneManager.LoadScene(MainSceneName);
    }
}
