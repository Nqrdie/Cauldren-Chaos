using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerRoot : MonoBehaviour
{
    [SerializeField] private GameObject[] playerRoots;
    private int connectedPlayers = 0;

    private void Awake()
    {
        foreach (GameObject playerRoot in playerRoots)
        {
            playerRoot.SetActive(false);
        }
    }

    public void SetPlayerRoot()
    {
        if (connectedPlayers < playerRoots.Length)
        {
            GameObject playerRoot = playerRoots[connectedPlayers];
            playerRoot.SetActive(true);
            connectedPlayers++;
        }
        else
        {
            Debug.LogWarning("Max players connected. Cannot assign more player roots.");
        }
    }
}


