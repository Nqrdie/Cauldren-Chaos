using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // when a player spawns have it added to a list,
    // if a player dies, have it get taken out of the list
    // if the list only contains 1 element, the game is over.
    private List<GameObject> players = new List<GameObject>();
    //[SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private Transform[] PlayerSpawns;
    [SerializeField] private GameObject playerPrefab;
    private void Start()
    {
        PlayerConfigManager.PlayerConfig[] playerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            GameObject GO = Instantiate(playerPrefab, PlayerSpawns[i].position, PlayerSpawns[i].rotation, gameObject.transform);
            GO.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            players.Add(GO);
        }

    }
    private void Update()
    {
        foreach (GameObject player in players)
        {
            if(!player.activeInHierarchy)
            {
                players.Remove(player);
                Destroy(player);
            }
        }

        if (players.Count <= 0)
        {
            //end game
        }
    }



    
}
