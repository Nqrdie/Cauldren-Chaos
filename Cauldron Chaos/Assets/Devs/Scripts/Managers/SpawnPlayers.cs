using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    private PlayerConfig playerConfig;

    public void initializePlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        //playerConfig.input.onActionTriggered += Input_onActionTriggered;
    }
}
