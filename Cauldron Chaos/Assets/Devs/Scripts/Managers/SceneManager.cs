using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    private const string MainSceneName = "Roy";
    private const string EndSceneName = "EndScene";
    public void LoadMainScene()
    {
        SceneManager.LoadScene(MainSceneName);
    }
    public void LoadEndScene()
    {
        SceneManager.LoadScene(EndSceneName);
    }
}
