using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
            //PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoin;
        }
    }

    public List<PConfig> GetPConfigs()
    {
        return playerConfigs;
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

    public void handlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined");
        int index = playerConfigs.Count;

        if (index < uiManagers.Count)
        {
            UIManager targetUIManager = uiManagers[index];
            targetUIManager.ShowConnectedUI();

            if (!playerConfigs.Any(p => p.playerIndex == index))
            {
                pi.transform.SetParent(transform);
                playerConfigs.Add(new PConfig(pi, targetUIManager) { playerIndex = index });
            }
        }
        else
        {
            Debug.LogError("Player index exceeds available UI Managers!");
        }
    }

}
