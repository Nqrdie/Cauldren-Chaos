using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement mover;
    private PlayerConfigManager.PlayerConfig playerConfig;

    [SerializeField] private MeshRenderer playerMesh;

    private CauldronChaos controls;


    private void Awake()
    {
        mover = GetComponent<PlayerMovement>();
        controls = new CauldronChaos();
    }
    public void InitializePlayer(PlayerConfigManager.PlayerConfig config)
    {
        playerConfig = config;
        playerMesh.material = config.playerMaterial;
        config.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Move.name)
        {
            OnMove(obj);
        }
    }

    public void OnMove(CallbackContext context)
    {
        Vector3 direction;
        direction = new Vector3(context.ReadValue<Vector3>().z, 0, -context.ReadValue<Vector3>().x);
        mover.SetInputVector(direction);
    }
}
