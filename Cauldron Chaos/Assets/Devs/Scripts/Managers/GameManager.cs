using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // when a player spawns have it added to a list,
    // if a player dies, have it get taken out of the list
    // if the list only contains 1 element, the game is over.
    private List<GameObject> players = new List<GameObject>();
    [SerializeField] private SceneChanger sceneChanger;

    private void Update()
    {
        if (players.Count <= 1)
        {
            sceneChanger.LoadEndScene();
        }
    }
}
