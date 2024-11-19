using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    private PConfig playerConfig;

    public void initializePlayer(PConfig pc)
    {
        playerConfig = pc;
        playerConfig.input.onActionTriggered += Input_onActionTriggered;
    }
}
