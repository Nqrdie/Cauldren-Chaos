using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PConfig : MonoBehaviour
{
    private InputManager input;
    [HideInInspector] public int playerIndex;
    [HideInInspector] public bool isReady;

    private void Start()
    {
        input = GetComponent<InputManager>();
    }
    public PConfig(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
    }
}
