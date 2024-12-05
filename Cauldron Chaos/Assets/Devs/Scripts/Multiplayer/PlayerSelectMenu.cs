using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectMenu : MonoBehaviour
{
    private int playerIndex;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button readyButton;
    [SerializeField] private GameObject[] heads = new GameObject[2];
    private int currentIndex = 0;



    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;
    public void setPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SelectHead()
    {
        if (!inputEnabled) { return; }
        PlayerConfigManager.instance.SetPlayerHead(playerIndex, heads[currentIndex]);
        readyPanel.SetActive(true);
        readyButton.interactable = true;
        menuPanel.SetActive(false);
        readyButton.Select();

    }

    public void ChangeHeadRight()
    {
        
        heads[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % heads.Length;
        heads[currentIndex].SetActive(true);
    }

    public void ChangeHeadLeft()
    {
        heads[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + heads.Length) % heads.Length;
        heads[currentIndex].SetActive(true);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }
        PlayerConfigManager.instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
        
    }
}