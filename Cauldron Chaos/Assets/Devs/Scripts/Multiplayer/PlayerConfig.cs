using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfig : MonoBehaviour
{
    public CauldronChaos input;
    [HideInInspector] public int playerIndex;
    [HideInInspector] public bool isReady;
    //public UIManager uiManager;

<<<<<<< Updated upstream
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public PConfig(PlayerInput pi, UIManager manager)
=======
    public PlayerConfig(PlayerInput pi)
>>>>>>> Stashed changes
    {
        //playerIndex = pi.playerIndex;
        //uiManager = manager;
        //input = GetComponent<>();
    }
}
