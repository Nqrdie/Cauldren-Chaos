using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject connectedText;
    [SerializeField] private GameObject readyText;
    [SerializeField] private Button readyButton;

    private void OnPlayerConnected()
    {
        connectedText.SetActive(true);
    }

    private void OnPlayerReady(int playerIndex)
    {
        readyText.SetActive(true);
        readyButton.Select();
        connectedText.SetActive(false);
        Debug.Log($"Player {playerIndex + 1} is ready!");
    }
}

