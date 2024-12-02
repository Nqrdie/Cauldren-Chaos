using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // when a player spawns have it added to a list,
    // if a player dies, have it get taken out of the list
    // if the list only contains 1 element, the game is over.
    private PlatformSink _platformSink;
    private ObjectsManager _objManager;
    private float timer = 0f;
    private List<GameObject> players = new List<GameObject>();
    //[SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject playerPrefab;
    private Coroutine spawnObjectsRoutine;
    private float objectDelay;
    private void Awake()
    {
        _platformSink = FindObjectOfType<PlatformSink>();
        _objManager = GetComponent<ObjectsManager>();
    }
    private void Start()
    {
        PlayerConfigManager.PlayerConfig[] playerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            GameObject GO = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            GO.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            players.Add(GO);
        }
    }
    private void Update()
    {
        timer += Time.deltaTime * 1;
        timer = Mathf.RoundToInt(timer);
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

        switch (timer) 
        {
            case 0f:
                StartCoroutine(SpawnObjects());
                objectDelay = 2.5f;
                break;
            case 20f:
                objectDelay = 2f;
                StartCoroutine(_platformSink.Sink());
                break;
            case 30f:
                objectDelay = 1.5f;
                //rotateplatform
                break;
            case 40f:
                objectDelay = 1f;
                StartCoroutine(_platformSink.Sink());
                break;
            case 50f:
                objectDelay = 0.8f;
                StartCoroutine(_platformSink.Sink());
                break;
            case 60f:
                objectDelay = 0.6f;
                StartCoroutine(_platformSink.Sink());
                break;
            case 70f:
                objectDelay = 0.5f;
                StartCoroutine(_platformSink.Sink());
                break;
            case 80f:
                StartCoroutine(_platformSink.Sink());
                break;  
        }



    }

    private IEnumerator SpawnObjects()
    {
        while (true) 
        {
            _objManager.SpawnObject();
            yield return new WaitForSeconds(objectDelay);
        }
    }

    
}
