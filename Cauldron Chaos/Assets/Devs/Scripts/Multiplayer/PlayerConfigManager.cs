using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfig> playerConfigs;
    [SerializeField] private int MaxPlayers = 4;

    private SceneChanger sceneChanger;
    private int playerIndex;
    public static PlayerConfigManager instance { get; private set; }
    //[SerializeField] private List<UIManager> uiManagers; 


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerConfig>();
            PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoin;
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
            if (!playerConfigs.Any(p => p.playerIndex == pi.playerIndex))
            {
                pi.transform.SetParent(transform);
                playerConfigs.Add(new PlayerConfig(pi));
            }
        else
        {
            Debug.LogError("Player index exceeds available UI Managers!");
        }
    }

    public List<PlayerConfig> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void ReadyPlayer(int index)
    {
        Debug.Log(index);
        playerConfigs[index].isReady = true;
        Debug.Log($"Player {index + 1} is ready!");

        //UIManager targetUIManager = playerConfigs[index].uiManager;
        //targetUIManager.ShowReadyUI();

        if (playerConfigs.All(p => p.isReady == true) && playerConfigs.Count >= 1)
        {
            SceneManager.LoadScene("Roy");
        }
    }

    public void SetPlayerColor(int index, Material color)
    {
        //playerConfigs[index].playerMaterial = color;
    }


    public class PlayerConfig
    {
        public PlayerConfig(PlayerInput pi)
        {
            playerIndex = pi.playerIndex;
            Input = pi;
        }

        public PlayerInput Input { get; private set; }
        public int playerIndex { get; private set; }
        public bool isReady { get; set; }
        public Material playerMaterial { get; set; }
    }

}
