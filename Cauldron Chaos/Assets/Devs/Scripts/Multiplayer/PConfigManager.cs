using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class PConfigManager : MonoBehaviour
{
    private List<PConfig> playerConfigs;
    [SerializeField] private int MaxPlayers = 4;

    private SceneChanger sceneChanger;
    private int playerIndex;
    public static PConfigManager instance;
    [SerializeField] private List<UIManager> uiManagers; 


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
            playerConfigs = new List<PConfig>();
            PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoin;
        }
    }

    public void ReadyPlayer(int index)
    {
        Debug.Log(index);
        playerConfigs[index].isReady = true;
        Debug.Log($"Player {index + 1} is ready!");

        UIManager targetUIManager = playerConfigs[index].uiManager;
        targetUIManager.ShowReadyUI();

        if (playerConfigs.All(p => p.isReady == true) && playerConfigs.Count >= 2)
        {
            sceneChanger.LoadMainScene();
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);

        if (pi.playerIndex < uiManagers.Count)
        {
            UIManager targetUIManager = uiManagers[pi.playerIndex];
            targetUIManager.ShowConnectedUI();

            if (!playerConfigs.Any(p => p.playerIndex == pi.playerIndex))
            {
                pi.transform.SetParent(transform);
                playerConfigs.Add(new PConfig(pi, targetUIManager)); 
            }
        }
        else
        {
            Debug.LogError("Player index exceeds available UI Managers!");
        }
    }


}
