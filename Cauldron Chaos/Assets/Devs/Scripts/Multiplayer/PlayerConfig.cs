using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfig : MonoBehaviour
{
    public CauldronChaos input;
    [HideInInspector] public int playerIndex;
    [HideInInspector] public bool isReady;
    public UIManager uiManager;

    public PlayerConfig(PlayerInput pi, UIManager manager)
    {
        //playerIndex = pi.playerIndex;
        uiManager = manager;
        //input = GetComponent<>();
    }
}
