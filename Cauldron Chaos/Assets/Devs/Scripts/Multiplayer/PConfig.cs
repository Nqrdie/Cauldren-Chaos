using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PConfig : MonoBehaviour
{
    private InputManager input;
    [HideInInspector] public int playerIndex;
    [HideInInspector] public bool isReady;
    public UIManager uiManager;

    private void Start()
    {
        input = GetComponent<InputManager>();
    }
    public PConfig(PlayerInput pi, UIManager manager)
    {
        playerIndex = pi.playerIndex;
        uiManager = manager;
    }
}
