using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    private const string StartSceneName = "Start Scene";
    private const string CharacterSelectSceneName = "Character Selection Scene";
    private const string SettingsSceneName = "Settings Scene";
    private const string CreditsSceneName = "Credits Scene";
    private const string MainSceneName = "Main Scene";
    private const string EndSceneName = "End Scene";

    public void LoadStartScene()
    {
        SceneManager.LoadScene(StartSceneName);
    }

    public void LoadCharacterSelectScene()
    {
        SceneManager.LoadScene(CharacterSelectSceneName);
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene(SettingsSceneName);
    }
    
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(CreditsSceneName);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(MainSceneName);
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(EndSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
