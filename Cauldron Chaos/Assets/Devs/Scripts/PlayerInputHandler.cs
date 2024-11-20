using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerConfig playerConfig;
    private PlayerMovement mover;
    private PlayerInput playerInput;

    private void Awake()
    {
        mover = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        var playerMovers = FindObjectsOfType<PlayerMovement>();
        var index = playerInput.playerIndex;
        mover = playerMovers.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }
    //public void InitializePlayer(PConfig pc)
    //{
    //    playerConfig = pc;
    //    //pc.Input.onActionTriggered += Input_onActionTriggered;
    //}

    //private void Input_onActionTriggered(CallbackContext obj)
    //{
    //    if (obj.action.name == playerInput.Move.name)
    //    {
    //        OnMove(obj);
    //    }
    //}

    public void OnMove(CallbackContext context)
    {
        Vector3 direction;
        direction = new Vector3(context.ReadValue<Vector3>().x, 0, context.ReadValue<Vector3>().z);
        mover.SetInputVector(direction);
    }
}
