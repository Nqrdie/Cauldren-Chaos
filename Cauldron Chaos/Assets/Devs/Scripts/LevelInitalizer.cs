using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitalizer : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject playerPrefab;

   /* private void Start()
    {
        PlayerConfig[] PConfigs = PlayerConfigManager.instance.GetPConfigs().ToArray();
        for (int i = 0; i < PConfigs.Length; i++)
        {
            GameObject player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(PConfigs[i]);
        }
    } */
}
