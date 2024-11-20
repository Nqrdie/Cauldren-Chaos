using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInput : MonoBehaviour
{

    private PConfig playerConfig;
    private PlayerMovement PlayerMovement;
    private CauldronChaos controls;

    private void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        controls = new CauldronChaos();
    }
    public void InitializePlayer(PConfig pc)
    {
        playerConfig = pc;
        //pc.input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Move.name)
        {
            OnMove(obj);
        }
    }

    private void OnMove(CallbackContext context)
    {
        if(PlayerMovement != null)
        {
            Vector3 direction;
            direction = new Vector3(context.ReadValue<Vector3>().x, 0, context.ReadValue<Vector3>().z);
            PlayerMovement.SetInputVector(direction);
        }
    }
}
