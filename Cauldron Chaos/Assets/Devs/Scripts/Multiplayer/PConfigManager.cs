using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class PConfigManager : MonoBehaviour
{
    private List<PConfig> playerConfigs;
    [SerializeField] private int MaxPlayers = 4;

    private SceneChanger sceneChanger;
    public static PConfigManager instance;

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

        }
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if (playerConfigs.All(p => p.isReady == true))
        {
            sceneChanger.LoadMainScene();
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        if(!playerConfigs.Any(p => p.playerIndex == pi.playerIndex)) 
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PConfig(pi));
        }
    }
}
