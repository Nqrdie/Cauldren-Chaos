using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfig> playerConfigs;

    private SceneChanger sceneChanger;
    private int playerIndex;
    public static PlayerConfigManager instance { get; private set; }

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

        if (playerConfigs.All(p => p.isReady == true) && playerConfigs.Count >= 1)
        {
            SceneManager.LoadScene("Main Scene");
        }
    }

    public void SetPlayerHead(int index, GameObject head)
    {
        playerConfigs[index].playerHead = head;
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
        public GameObject playerHead { get; set; }
    }

}
