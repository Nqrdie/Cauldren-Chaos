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
    [SerializeField] private GameObject[] headSelection = new GameObject[2];
    public Sprite[] sprites;
    public GameObject image;
    private int currentIndex = 0;



    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    private void Start()
    {
        SetPlayerImage();
    }
    public void setPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    private void SetPlayerImage()
    {
        image.GetComponent<Image>().sprite = sprites[playerIndex];
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

        headSelection[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % heads.Length;
        headSelection[currentIndex].SetActive(true);
    }

    public void ChangeHeadLeft()
    {
        headSelection[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + heads.Length) % heads.Length;
        headSelection[currentIndex].SetActive(true);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }
        PlayerConfigManager.instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
        
    }
}