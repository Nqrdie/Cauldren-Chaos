using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerMenu : MonoBehaviour
{
    public GameObject playerMenu;
    public PlayerInput input;
    private GameObject layout;
    private void Awake()
    {

        layout = GameObject.Find("Layout");
        if (layout != null)
        {
            var menu = Instantiate(playerMenu, layout.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSelectMenu>().setPlayerIndex(input.playerIndex);
        }
    }
}
