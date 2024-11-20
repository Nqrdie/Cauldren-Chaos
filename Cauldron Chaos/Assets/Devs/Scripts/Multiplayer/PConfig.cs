using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PConfig : MonoBehaviour
{
    public CauldronChaos input;
    [HideInInspector] public int playerIndex;
    [HideInInspector] public bool isReady;
    public UIManager uiManager;

    public PConfig(PlayerInput pi, UIManager manager)
    {
        //playerIndex = pi.playerIndex;
        uiManager = manager;
        input = GetComponent <CauldronChaos>();
    }
}
